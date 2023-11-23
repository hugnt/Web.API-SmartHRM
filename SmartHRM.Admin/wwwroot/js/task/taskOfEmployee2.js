﻿import { htmlText, huGrid } from '../hugrid.js';
import * as API from '../api.js';
import * as AJAXCONFIG from '../ajax_config.js';
import '../layout/jqueryCookie.js'
import jwt_decode from '../layout/decodeJWT.js';

$(document).ready(async function () {
    const choicesEmployee = new Choices(document.querySelector('#assigneeId'));
    const choicesAssignerId = new Choices(document.querySelector('#assignerId'));
    const choicesTask = new Choices(document.querySelector('#taskId'));
    await loadSelectBox();

    var accessToken = $.cookie('AccessToken');
    var decoded = jwt_decode(accessToken);

    const ACCOUNT_ID = decoded.Id;
    console.log(ACCOUNT_ID);

    const ACCOUNT_INFOR = await getData(`/Account/AccountInfor/${ACCOUNT_ID}`);
    console.log(ACCOUNT_INFOR);

    var listData = await getList(`/TaskDetails/EmployeeDepartment/${ACCOUNT_INFOR.employeeId}`);
    console.log(listData);
    var columns = [
        {
            id: "id",
            name: htmlText(`<div class="text-center">Id</div>`),
            sort: true,
            formatter: function (e) {
                return htmlText(`<div class="text-center">${e}</div>`)
            },
            width: '8%'
        },
        {
            id: "assigneeName",
            name: "Assgnee Name",
            sort: true
        },
        {
            id: "assignerName",
            name: "Assgner Name",
            sort: true
        },
        {
            id: "taskName",
            name: "Task Name",
            sort: true
        },
        {
            id: "content",
            name: "Content",
            sort: true
        },
        {
            id: "description",
            name: "Description",
            sort: true
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


    //1st render
    var newGrid = new huGrid("table-product-list-all", columns, listData);
    newGrid.addEventListener(".btnDetails", getDetails);
    newGrid.addEventListener(".btnUpdate", updateInfor);
    newGrid.addEventListener(".btnDelete", deleteInfor);

    //Modal
    const myModal = new bootstrap.Modal(document.getElementById('inforModal'));
    document.getElementById('inforModal').addEventListener('hidden.bs.modal', event => {
        setStatusForm(true);

    })

    //Details
    async function getDetails() {
        var id = $(this).data("id");
        var selectedData = await getData(`/TaskDetails/${id}`);
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
        await postData("/TaskDetails", dataInsert);
        var listData = await getList("/TaskDetails/EmployeeDepartment/4");
        newGrid.updateData(listData)
    });


    //Update
    async function updateInfor() {
        var id = $(this).data("id");
        var selectedData = await getData(`/TaskDetails/${id}`);
        setDataForm(selectedData);
        setTypeForm("edit");
        myModal.show()
    };
    $("#btnSave").click(async function () {
        var updatedData = getDataForm();
        console.log(updatedData)
        if (!await isValidForm(updatedData, "update")) return;
        myModal.hide();
        await putData(`/TaskDetails/${updatedData.id}`, updatedData);
        var listData = await getList("/TaskDetails");
        newGrid.updateData(listData)
    });


    //Remove
    async function deleteInfor() {
        var id = $(this).data("id");
        localStorage.setItem("selectedId", id)
    };
    $("#delete-notification").click(async function () {
        var id = localStorage.getItem("selectedId");
        await deleteData(`/TaskDetails/${id}`);
        var listData = await getList("/TaskDetails");
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
            await deleteData(`/TaskDetails/${listId[i]}`);
        };
        var listData = await getList("/TaskDetails");
        newGrid.updateData(listData);
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
        var listData = await getList(`/TaskDetails/Search/${field}/${keyword}`);
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

    //Form action
    function getDataForm() {
        //console.log(pond)
        return {
            id: $("#taskDetailsId").val() == "" ? 0 : $("#taskDetailsId").val(),
            assigneeId: choicesEmployee.getValue(true),
            assignerId: choicesAssignerId.getValue(true),
            taskId: choicesTask.getValue(true),
            content: $("#content").val(),
            description: $("#description").val(),
            isDeleted: false,
        }
    }

    function setDataForm(data) {
        if (data == null) {
            $("#inforModal form :input").val("");
            choicesEmployee.setChoiceByValue("");
            choicesAssignerId.setChoiceByValue("");
            choicesTask.setChoiceByValue("");
            return;
        }
        $("#taskDetailsId").val(data.id);
        choicesEmployee.setChoiceByValue(data.assigneeId);
        choicesAssignerId.setChoiceByValue(data.assignerId);
        choicesTask.setChoiceByValue(data.taskId);
        $("#content").val(data.content);
        $("#description").val(data.description);
    }

    function setStatusForm(status) {
        $("#inforModal form :input").prop("disabled", !status);
        $('input[name="id"]').prop("disabled", true);
        if (status == false) {
            choicesEmployee.disable();
            choicesAssignerId.disable();
            choicesTask.disable();
        }
        else {
            choicesEmployee.enable();
            choicesAssignerId.enable();
            choicesTask.enable();
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

    async function isValidForm(data, when = null) {
        var message = null;
        if (data.assigneeId == "" || data.assigneeId == null) {
            message = "Assignee is required!";
        }
        if (data.assignerId == "" || data.assignerId == null) {
            message = "Assigner is required!";
        }
        else if (data.taskId == "" || data.taskId == null) {
            message = "Task is required!";
        }
        var lstData = await getList("/TaskDetails");
        var filterData = lstData.filter(x => x.assigneeId == data.assigneeId
            && x.taskId == data.taskId
            && new Date(x.provideAt).getMonth() == new Date(data.provideAt).getMonth()
            && new Date(x.provideAt).getFullYear() == new Date(data.provideAt).getFullYear());
        if (when != "update" && filterData.length > 0) {
            message = "Task for Employee in this month already exist!";
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

    async function loadSelectBox() {
        var lstEmployee = await getList("/Employee");
        var lstTask = await getList("/Task");

        var lstEmployeeChoices = [];
        var lstTaskChoices = [];
        for (var i = 0; i < lstEmployee.length; i++) {
            var obj = lstEmployee[i];
            lstEmployeeChoices.push({ value: obj.id, label: obj.fullName })
        }
        for (var i = 0; i < lstTask.length; i++) {
            var obj = lstTask[i];
            lstTaskChoices.push({ value: obj.id, label: obj.name })
        }
        choicesTask.setChoices(lstTaskChoices)
        choicesEmployee.setChoices(lstEmployeeChoices)
        choicesAssignerId.setChoices(lstEmployeeChoices)

    }

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