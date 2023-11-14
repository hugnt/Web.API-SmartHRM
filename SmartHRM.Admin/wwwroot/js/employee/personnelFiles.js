import { htmlText, huGrid } from '../hugrid.js';
import * as API from '../api.js';
import * as AJAXCONFIG from '../ajax_config.js';

$(document).ready(async function () {
    var listData = await getList("/Employee");
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
            id: "fullName",
            name: "Full Name",
            sort: true,
            data: function (e) {
                return htmlText(`
                    <div class= "d-flex align-items-center" >
                        <div class="flex-shrink-0 me-3">
                            <div class="avatar-sm bg-light rounded p-1">
                                <img src="${API.IMAGE_URL}/avatar/${e.avatar}" alt="" class="img-fluid d-block">
                            </div >
                        </div>
                        <div class="flex-grow-1">
                            <h5 class="fs-14 mb-1">
                                <a href="apps-ecommerce-product-details.html" class="text-body">${e.fullName}</a>
                            </h5>
                            <p class="text-muted mb-0">Department : <span class="fw-medium">${e.departmentId}</span></p>
                        </div >
                    </div > `)
            }
        },
        {
            id: "phoneNumber",
            name: "Phone Number"
        },
        {
            id: "email",
            name: "Email"
        },
        {
            id: "dob",
            name: "Date of birth",
            formatter: function (e) {
                return new Date(e).toLocaleDateString();
            }
        },
        {
            id: "gender",
            name: "Gender",
            formatter: function (e) {
                return e ? "Male" : "Female";
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
        var selectedData = await getData(`/Employee/${id}`);
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
        await postData("/Employee", dataInsert);
        var listData = await getList("/Employee");
        newGrid.updateData(listData)
    });


    //Update
    async function updateInfor() {
        var id = $(this).data("id");
        var selectedData = await getList(`/Employee/${id}`);
        setDataForm(selectedData);
        setTypeForm("edit");
        myModal.show()
    };
    $("#btnSave").click(async function () {
        var updatedData = getDataForm();
        console.log(updatedData)
        if (!await isValidForm(updatedData)) return;
        myModal.hide();
        await putData(`/Employee/${updatedData.id}`, updatedData);
        var listData = await getList("/Employee");
        newGrid.updateData(listData)
    });


    //Remove
    async function deleteInfor() {
        var id = $(this).data("id");
        localStorage.setItem("selectedId",id)
    };
    $("#delete-notification").click(async function () {
        var id = localStorage.getItem("selectedId");
        await putStatus(`/Employee/DeletedStatus/${id}/${true}`);
        var listData = await getList("/Employee");
        newGrid.updateData(listData)
    });


    $(".btnTestModal").click(function () {
        
    });

    //Export
    $("#btnExport").click(function () {
        $("table").table2excel({
            name: "Worksheet Name",
            filename: "exportList.xls",
        });
    });

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
        return {
            id: $("#employee_id").val() == "" ? 0 : $("#employee_id").val(),
            fullName: $("#fullName").val(),
            phoneNumber: $("#phoneNumber").val(),
            email: $("#email").val(),
            dob: $("#dob").val(),
            address: $("#address").val(),
            avatar: pond.getFiles().length != 0 ? pond.getFile().filename : null,
            gender: $("#gender").val() == "1" ? true : false,
            departmentId: $("#departmentId").val(),
            positionId: $("#positionId").val(),
            identificationCard: $("#identificationCard").val()
        }
    }

    function setDataForm(data) {
        if (data == null) {
            $("#inforModal form :input").val("");
            pond.removeFile();
            return;
        }
        $("#employee_id").val(data.id);
        $("#fullName").val(data.fullName);
        $("#phoneNumber").val(data.phoneNumber);
        $("#email").val(data.email);
        $("#dob").val(data.dob.substring(0, 10));
        $("#address").val(data.address);
        $("#gender").val(data.gender ? "1" : "0");
        $("#departmentId").val(data.departmentId);
        $("#positionId").val(data.positionId);
        $("#identificationCard").val(data.identificationCard);
        data.avatar && pond.addFile(`${API.IMAGE_URL}/avatar/${data.avatar}`)
            .then((file) => {
                // File has been added
            })
            .catch((error) => {
                // Something went wrong
                console.log(error)
            });

    }

    function setStatusForm(status) {
        $("#inforModal form :input").prop("disabled", !status);
        $('input[name="id"]').prop("disabled", true);
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
        const phoneNumberRegex = /^[+]?[(]?[0-9]{1,4}[)]?[-\s./0-9]*$/;
        const emailRegex = /^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$/;
        const numberOnlyRegex = /^\d+$/;
        if (data.fullName == "" || data.fullName ==null) {
            message = "Full name is required!";
        }
        else if (!phoneNumberRegex.test(data.phoneNumber)) {
            message = "phone number is not valid!";
        }
        else if (!emailRegex.test(data.email)) {
            message = "email is not valid!";
        }
        else if (!numberOnlyRegex.test(data.identificationCard)) {
            message = "Identification Card is not valid!";
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



    //File handler
    FilePond.registerPlugin(
        // encodes the file as base64 data
        FilePondPluginFileEncode,
        // validates the size of the file
        FilePondPluginFileValidateSize,
        // corrects mobile image orientation
        FilePondPluginImageExifOrientation,
        // previews dropped images
        FilePondPluginImagePreview
    );

    const pond = FilePond.create(
        document.querySelector('.filepond-input-circle'),
        {
            labelIdle: 'Drag & Drop your picture or Browse',
            imagePreviewHeight: 170,
            imageCropAspectRatio: '1:1',
            imageResizeTargetWidth: 200,
            imageResizeTargetHeight: 200,
            stylePanelLayout: 'compact circle',
            styleLoadIndicatorPosition: 'center bottom',
            styleProgressIndicatorPosition: 'right bottom',
            styleButtonRemoveItemPosition: 'left bottom',
            styleButtonProcessItemPosition: 'right bottom',
        }
    );
    
});