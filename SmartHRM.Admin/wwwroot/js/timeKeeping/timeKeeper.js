import { htmlText, huGrid } from '../hugrid.js';
import * as API from '../api.js';
import * as AJAXCONFIG from '../ajax_config.js';

$(document).ready(async function () {
    var listData = await getList("/TimeKeeping");
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
            id: "employeeDetail",
            name: "Employee Name",
            formatter: function (e) {
                return e.fullName;
            },
        },

        {
            id: "timeAttendance",
            name: "Time Attendance",
            formatter: function (e) {
                return toDateTime(e);
            }
        },
        {
            id: "note",
            name: "Note"
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
    const choicesEmployee = new Choices(document.querySelector('#employeeId'));
    await loadSelectBox();
    document.getElementById('inforModal').addEventListener('hidden.bs.modal', event => {
        setStatusForm(true);

    })

    //Details
    async function getDetails() {
        var id = $(this).data("id");
        var selectedData = await getData(`/TimeKeeping/${id}`);
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
        await postData("/TimeKeeping", dataInsert);
        var listData = await getList("/TimeKeeping");
        newGrid.updateData(listData)
    });


    //Update
    async function updateInfor() {
        var id = $(this).data("id");
        var selectedData = await getData(`/TimeKeeping/${id}`);
        setDataForm(selectedData);
        setTypeForm("edit");
        myModal.show()
    };
    $("#btnSave").click(async function () {
        var updatedData = getDataForm();
        console.log(updatedData)
        if (!await isValidForm(updatedData)) return;
        myModal.hide();
        await putData(`/TimeKeeping/${updatedData.id}`, updatedData);
        var listData = await getList("/TimeKeeping");
        newGrid.updateData(listData)
    });


    //Remove
    async function deleteInfor() {
        var id = $(this).data("id");
        localStorage.setItem("selectedId", id)
    };
    $("#delete-notification").click(async function () {
        var id = localStorage.getItem("selectedId");
        await putStatus(`/TimeKeeping/DeletedStatus/${id}/${true}`);
        var listData = await getList("/TimeKeeping");
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
        var listData = await getList(`/TimeKeeping/Search/${field}/${keyword}`);
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
                    Deduction: "top",
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
                Deduction: "top",
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
        return {
            id: $("#timeKeeper_id").val() == "" ? 0 : $("#timeKeeper_id").val(),
            employeeId: choicesEmployee.getValue(true),
            timeAttendance: $("#timeAttendance").val(),
            note: $("#note").val(),
            isDeleted: false,
        }
    }

    function setDataForm(data) {
        if (data == null) {
            $("#inforModal form :input").val("");
            choicesEmployee.setChoiceByValue("");
            return;
        }
        choicesEmployee.setChoiceByValue(data.employeeDetail.id);
        $("#timeKeeper_id").val(data.id);
        $("#timeAttendance").val(data.timeAttendance);
        $("#note").val(data.note);
    }

    function setStatusForm(status) {
        $("#inforModal form :input").prop("disabled", !status);
        $('input[name="id"]').prop("disabled", true);
        if (status == false) {
            choicesEmployee.disable();
        }
        else {
            choicesEmployee.enable();

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
    async function loadSelectBox() {
        var lstEmployee = await getList("/Employee");

        var lstEmployeeChoices = [];
        console.log(lstEmployee);
        for (var i = 0; i < lstEmployee.length; i++) {
            var obj = lstEmployee[i];
            lstEmployeeChoices.push({ value: obj.id, label: obj.fullName })
        }
        choicesEmployee.setChoices(lstEmployeeChoices)

    }

    async function isValidForm(data) {
        var message = null;
        if (data.timeAttendance == "" || data.timeAttendance == null) {
            message = "Time Attendance is required!";
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