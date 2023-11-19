﻿import { htmlText, huGrid } from '../hugrid.js';
import * as API from '../api.js';
import * as AJAXCONFIG from '../ajax_config.js';

$(document).ready(async function () {
    var listData = await getList("/Project");
    console.log(listData);
    var columns = [
        {
            id: "id",
            name: htmlText(`<div class="text-center">Id</div>`),
            sort: false,
            formatter: function (e) {
                return htmlText(`<div class="text-center">${e}</div>`)
            }
        },
        {
            id: "name",
            name: "Name",
            sort: true
        },
        {
            id: "leader_id",
            name: "Leader ID"
        },

        {
            id: "status",
            name: "Status"
        },

        {
            id: "startedAt",
            name: "StartedAt",
            formatter: function (e) {
                return toDateTime(e)
            }
        },

        {
            id: "endedAt",
            name: "EndedAt",
            formatter: function (e) {
                return toDateTime(e)
            }
        },

        {
            id: 'Action',
            name: htmlText('<div class="text-center noExl">Action</div>'),
            sort: false,
            formatter: function (e, t) {
                //t = (new DOMParser).parseFromString(t._cells[0].data.props.content, "text/html").body.querySelector(".checkbox-product-list .form-check-input").value;
                return htmlText(`
            <div class="dropdown text-center noExl">
                <button class="btn btn-soft-secondary btn-sm dropdown" type="button" data-bs-toggle="dropdown" aria-expanded="false"><i class="ri-more-fill"></i></button>
                <ul class="dropdown-menu dropdown-menu-end">
                    <li class="btnRestore" data-id=${t._cells[1].data}>
                        <a class="dropdown-item"   data-bs-toggle="modal" data-bs-target="#restoreNotificationModal">
                        <i class="ri-refresh-line align-bottom me-2 text-muted"></i> Restore</a>
                    </li>
                    <li class="dropdown-divider"></li>
                    <li class="btnDelete" data-id=${t._cells[1].data}>
                        <a class="dropdown-item" data-bs-toggle="modal" data-bs-target="#removeNotificationModal">
                            <i class="ri-delete-bin-fill align-bottom me-2 text-muted"></i> Delete</a>
                    </li>
                </ul>
            </div>`)
            }
        }];

    //1st render
    var newGrid = new huGrid("table-product-list-all", columns, listData);
    newGrid.addEventListener(".btnRestore", restoreInfor);
    newGrid.addEventListener(".btnDelete", deleteInfor);

    //Restore
    async function restoreInfor() {
        var id = $(this).data("id");
        localStorage.setItem("selectedId", id)
    };
    $("#restore-notification").click(async function () {
        var id = localStorage.getItem("selectedId");
        await putStatus(`/Project/DeletedStatus/${id}/${false}`);
        var listData = await getList("/Project");
        newGrid.updateData(listData);
    });
    $("#btnRestoreListCheck").click(async function () {
        if (newGrid.getListSelectedId().length == 0) {
            Toastify({
                text: "Please choose at least 1 item!",
                close: true,
                className: "bg-danger",
                duration: 1500
            }).showToast();
            return;
        }
        var listId = newGrid.getListSelectedId();
        for (var i = 0; i < listId.length; i++) {
            await putStatus(`/Project/DeletedStatus/${listId[i]}/${false}`);
        };
        var listData = await getList("/Project");
        newGrid.updateData(listData);
    });


    //Remove
    async function deleteInfor() {
        var id = $(this).data("id");
        localStorage.setItem("selectedId",id)
    };
    $("#delete-notification").click(async function () {
        var id = localStorage.getItem("selectedId");
        await deleteData(`/Project/${id}`);
        var listData = await getList("/Project");
        newGrid.updateData(listData)
    });
    $("#btnRemoveListCheck").click(async function () {
        if (newGrid.getListSelectedId().length == 0) {
            Toastify({
                text: "Please choose at least 1 item!",
                close: true,
                className: "bg-danger",
                duration: 1500
            }).showToast();
            return;
        }
        var listId = newGrid.getListSelectedId();
        for (var i = 0; i < listId.length; i++) {
            await deleteData(`/Project/${listId[i]}`);
        };
        var listData = await getList("/Project");
        newGrid.updateData(listData);
    });
   
    $(".btnTestModal").click(function () {
        Toastify({
            text: "This is a toast",
            close: true,
            className: "bg-success",
            duration: 1500
        }).showToast();
    });

    //Export
    $("#btnExport").click(function () {
        $("table").table2excel({
            name: "Worksheet Name",
            filename: "exportList.xls",
        });
    });

    //Search
    $("#btnFilter").click(async function () {
        var field = $("#sellectBoxFilter").val();
        var keyword = $("#searchBoxFilter").val();
        keyword = keyword == "" ? null : keyword;
        console.log(field + " - keyword: " + keyword)
        var listData = await getList(`/Project/Search/${field}/${keyword}`);
        console.log(listData)
        newGrid.updateData(listData)
        if (listData.length == 0) $(".noresult").show();
        else $(".noresult").hide()

    })

    //Ajax
    async function getList(endPoint) {
        try {
            const res = await $.ajax({
                url: `${API.API_URL}${endPoint}`,
                type: "GET",
                contentType: "application/json",
                beforeSend: function (xhr) {
                    AJAXCONFIG.ajaxBeforeSend(xhr, false);
                }
            });
            if (res) {
                return res.filter(x => x.isDeleted === true);
            }
        } catch (e) {
            console.log(e);
            AJAXCONFIG.ajaxFail(e);
        }
        finally {
            AJAXCONFIG.ajaxAfterSend();
        }
    }

    async function putStatus(endPoint) {
        try {
            const res = await $.ajax({
                url: `${API.API_URL}${endPoint}`,
                type: "PUT",
                contentType: "application/json",
                beforeSend: function (xhr) {
                    AJAXCONFIG.ajaxBeforeSend(xhr, false);
                }
            });
            Toastify({
                text: "Restore successfully!",
                close: true,
                className: "bg-success",
                duration: 1500
            }).showToast();
        } catch (e) {
            console.log(e);
            Swal.fire({
                icon: "error",
                title: "Oops...",
                text: "Something went wrong!",
            });
        }
        finally {
            AJAXCONFIG.ajaxAfterSend();
        }
    }

    async function deleteData(endPoint) {
        try {
            const res = await $.ajax({
                url: `${API.API_URL}${endPoint}`,
                type: "DELETE",
                contentType: "application/json",
                beforeSend: function (xhr) {
                    AJAXCONFIG.ajaxBeforeSend(xhr, false);
                }
            });
            Toastify({
                text: "Data has been deleted!",
                close: true,
                className: "bg-success",
                duration: 1500
            }).showToast();
        } catch (e) {
            console.log(e);
            Swal.fire({
                icon: "error",
                title: "Oops...",
                text: "Something went wrong!",
            });
        }
        finally {
            AJAXCONFIG.ajaxAfterSend();
        }
    }
    //DateTime Format
    function toDateTime(stringDateTime) {
        var now = new Date(stringDateTime);

        var day = now.getDate();
        var month = now.getMonth() + 1;
        var year = now.getFullYear();

        var hours = now.getHours();
        var minutes = now.getMinutes();

        var formattedDate = day + '/' + month + '/' + year;
        var formattedTime = hours + ':' + (minutes < 10 ? '0' + minutes : minutes);

        return formattedDate + '  ' + formattedTime;
    }
    
});