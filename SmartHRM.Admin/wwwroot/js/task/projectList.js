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
            id:"name",
            name: "Name",
            sort: true 
        },
        {
            id: "leader_id",
            name: "Leader ID"
        },
        
        {
            id: "status",
            name: "Status",
            formatter: function (e) {
                return toStatus(e)
            }
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
            name: htmlText('<div class="text-center">Action</div>'),
            sort: false,
            formatter: function (e, t) {
                //t = (new DOMParser).parseFromString(t._cells[0].data.props.content, "text/html").body.querySelector(".checkbox-product-list .form-check-input").value;
                return htmlText(`
            <div class="dropdown text-center noExl">
                <button class="btn btn-soft-secondary btn-sm dropdown" type="button" data-bs-toggle="dropdown" aria-expanded="false"><i class="ri-more-fill"></i></button>
                <ul class="dropdown-menu dropdown-menu-end">
                    <li class="btnDetails" data-id=${t._cells[1].data}>
                        <a class="dropdown-item">
                        <i class="ri-eye-fill align-bottom me-2 text-muted"></i> View</a>
                    </li>
                    <li class="btnUpdate" data-id=${t._cells[1].data}>
                        <a class="dropdown-item">
                        <i class="ri-pencil-fill align-bottom me-2 text-muted"></i> Edit</a>
                    </li>
                    <li class="dropdown-divider"></li>
                    <li class="btnDelete" data-id=${t._cells[1].data}>
                        <a class="dropdown-item " href="#"  data-bs-toggle="modal" data-bs-target="#removeNotificationModal">
                            <i class="ri-delete-bin-fill align-bottom me-2 text-muted"></i> Delete</a>
                    </li>
                </ul>
            </div>`)
            }
        }];

    var newGrid = new huGrid("table-product-list-all", columns, listData);
    newGrid.addEventListener(".btnDetails", getDetails);
    newGrid.addEventListener(".btnUpdate", updateInfor);
    newGrid.addEventListener(".btnDelete", deleteInfor);

    //Modal
    const choicesStatus = new Choices(document.querySelector('#projectStatus'));
    await loadSelectBox()
    const myModal = new bootstrap.Modal(document.getElementById('inforModal'));
    document.getElementById('inforModal').addEventListener('hidden.bs.modal', event => {
        setStatusForm(true);

    })

    //Details
    async function getDetails() {
        var id = $(this).data("id");
        var selectedData = await getData(`/Project/${id}`);
        setDataForm(selectedData);
        setTypeForm("details");
        setStatusForm(false);
        myModal.show()
    };

    //Add
    $(".btnAddNew").click(function () {
        setDataForm(null);
        setTypeForm("add");
        myModal.show();
    });
    $("#btnAdd").click(async function () {
        var dataInsert = getDataForm();
        if (!await isValidForm(dataInsert)) return;
        myModal.hide();
        await postData("/Project", dataInsert);
        var listData = await getList("/Project");
        newGrid.updateData(listData)
    });

    //Update
    async function updateInfor() {
        var id = $(this).data("id");
        var selectedData = await getData(`/Project/${id}`);
        setDataForm(selectedData);
        setTypeForm("edit");
        myModal.show()
    };
    $("#btnSave").click(async function () {
        var updatedData = getDataForm();
        console.log(updatedData)
        if (!await isValidForm(updatedData)) return;
        myModal.hide();
        await putData(`/Project/${updatedData.id}`, updatedData);
        var listData = await getList("/Project");
        newGrid.updateData(listData)
    });


    //Remove
    async function deleteInfor() {
        var id = $(this).data("id");
        localStorage.setItem("selectedId", id)
    };
    $("#delete-notification").click(async function () {
        var id = localStorage.getItem("selectedId");
        await putStatus(`/Project/DeletedStatus/${id}/${true}`);
        var listData = await getList("/Project");
        newGrid.updateData(listData)
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
                return res.filter(x => x.isDeleted === false);
            }
        } catch (e) {
            console.log(e);
            AJAXCONFIG.ajaxFail(e);
        }
        finally {
            AJAXCONFIG.ajaxAfterSend();
        }
    }

    async function getData(endPoint) {
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
                return res;
            }
        } catch (e) {
            console.log(e);
            AJAXCONFIG.ajaxFail(e);
        }
        finally {
            AJAXCONFIG.ajaxAfterSend();
        }
    }

    async function postData(endPoint, data) {
        try {
            const res = await $.ajax({
                url: `${API.API_URL}${endPoint}`,
                type: "POST",
                contentType: "application/json",
                data: JSON.stringify(data),
                beforeSend: function (xhr) {
                    AJAXCONFIG.ajaxBeforeSend(xhr, false);
                }
            });
            if (res) {
                Swal.fire({
                    position: "top",
                    icon: "success",
                    title: "Created Successfully",
                    showConfirmButton: false,
                    timer: 1500
                });
            }
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

    async function putData(endPoint, data) {
        try {
            const res = await $.ajax({
                url: `${API.API_URL}${endPoint}`,
                type: "PUT",
                contentType: "application/json",
                data: JSON.stringify(data),
                beforeSend: function (xhr) {
                    AJAXCONFIG.ajaxBeforeSend(xhr, false);
                }
            });
            Swal.fire({
                position: "top",
                icon: "success",
                title: "Updated Successfully",
                showConfirmButton: false,
                timer: 1500
            });
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
                text: "Data has been moved to trash!",
                close: true,
                className: "bg-success",
                duration: 2000
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

    //Form action
    function getDataForm() {
        //console.log(pond)
        var statusVal = choicesStatus.getValue(true);
        if (statusVal == "" || statusVal == null) {
            statusVal = 0;
        }
              
        return {
            id: $("#project_id").val() == "" ? 0 : $("#project_id").val(),
            name: $("#name").val(),
            leader_id: $("#leader_id").val(),
            startedAt: $("#startedAt").val(),
            endedAt: $("#endedAt").val() == "" ? null : $("#endedAt").val(),
            status: statusVal,
            isDeleted: false,
        }
    }

    function setDataForm(data) {
        if (data == null) {
            $("#inforModal form :input").val("");
            return;
        }        
        var endDate = data.endedAt != null ? data.endedAt.split("T")[0] : null
       
        choicesStatus.setChoiceByValue(data.status);

        $("#project_id").val(data.id);
        $("#name").val(data.name);
        $("#leader_id").val(data.leader_id);
        $("#startedAt").val(data.startedAt.split("T")[0]);
        endDate == null ? $("#endedAt").val(data.endedAt) : $("#endedAt").val(data.endedAt.split("T")[0]);
    }

    function setStatusForm(status) {
        $("#inforModal form :input").prop("disabled", !status);
        $('input[name="id"]').prop("disabled", true);
        if (status == false) {
            choicesStatus.disable();
        }
        else {
            choicesStatus.enable();
        }
    }

    function setTypeForm(type) {
        $("#btnSave").hide();
        $("#btnAdd").hide();
        if (type == "add") {
            $("#btnAdd").show();
        }
        else if (type == "edit") {
            $("#btnSave").show();
        }

    }

    async function isValidForm(data) {
        var message = null;
        if (data.name == "" || data.name == null) {
            message = "Project name is required!";
        }
        else if (data.leader_id == "" || data.leader_id == null) {
            message = "Project ID of Leader is required!";
        }
        else if (data.startedAt == "" || data.startedAt == null) {
            message = "Started date is required!";
        }
        if (message != null) {
            Toastify({
                text: message,
                close: true,
                className: "bg-danger",
                duration: 2500
            }).showToast();
            return false;
        }
        await Toastify({
            text: 'Validated Successfully',
            close: true,
            className: "bg-success",
            duration: 1500
        }).showToast();
        return true;
    }

    //DateTime Format
    function toDateTime(stringDateTime) {
        var now = new Date(stringDateTime);
        
            var day = now.getDate();
            var month = now.getMonth() + 1;
            var year = now.getFullYear();


            var hours = now.getHours();
            var minutes = now.getMinutes();

            var formattedDate = month + '/' + day + '/' + year;
            var formattedTime = hours + ':' + (minutes < 10 ? '0' + minutes : minutes);

            return formattedDate + '  ' + formattedTime;
    }

    //Status format
    function toStatus(status) {
        var s = status;
        if (s == 0) return "Not started";
        if (s == 1) return "In progress";
        if (s == 2) return "Finished";
    }

    //load select box
    async function loadSelectBox() {


        var lstStatusChoices = [
            { value: 0, label: "Not started" },
            { value: 1, label: "In progress" },
            { value: 2, label: "Finished" }
        ];

        choicesStatus.setChoices(lstStatusChoices)

    }

}
);