import { htmlText, huGrid } from '../hugrid.js';
import * as API from '../api.js';
import * as AJAXCONFIG from '../ajax_config.js';

$(document).ready(async function () {
    const choicesAllowances = new Choices(document.querySelector('#allowances'), { removeItems: true, removeItemButton: true });
    const choicesBonus = new Choices(document.querySelector('#bonuss'), { removeItems: true, removeItemButton: true});
    const choicesDeduction = new Choices(document.querySelector('#deductions'), { removeItems: true, removeItemButton: true});
    const choicesEmployee = new Choices(document.querySelector('#employee'));
    await loadSelectBox();
    var allowanceDetailsId = [];
    var bonusDetailsId = [];
    var deductionDetailsId = [];
    choicesEmployee.passedElement.element.addEventListener(
        'change',
        function (event) {
            $("#employeeId").val(event.detail.value)
            console.log(event.detail.value);
        },
        false,
    );
    $("#monthYear").change(async function () {
        var inputDate = $(this).val();
        var parts = inputDate.split('-');
        var year = parts[0];
        var month = parts[1];
        var monthYear = month + "%2F" + year;
        var employeeId = $("#employeeId").val();
        console.log(new Date($("#monthYear").val()).toLocaleDateString());
        await loadSelectBox(true);
        var selectedSalary = await getData(`/Salary/ListSalary/${employeeId}/${monthYear}`);
        console.log(selectedSalary)
        if (selectedSalary != undefined || selectedSalary != null) {
            setDataForm(selectedSalary)
        }
        
    });

    
    var listData = await getList("/Salary/ListSalary");
    console.log(listData);
    var columns = [
        {
            id: "id",
            name: htmlText(`<div class="text-center">Id</div>`),
            sort: true,
            formatter: function (e) {
                return htmlText(`<div class="text-center">${e}</div>`)
            }
        },
        {
            id: "fullName",
            name: "Employee Name",
            sort: true
        },
        {
            id: "monthYear",
            name: "Month/Year",
            sort: true
        },
        {
            id: "basicSalary",
            name: "Basic Salary",
            sort: true
        },
        {
            id: "coefficientSalary",
            name: htmlText(`<div class="text-center">Coeff</div>`),
            sort: true,
            formatter: function (e) {
                return htmlText(`<div class="text-center">${e}</div>`)
            }
        },
        {
            id: "allowances",
            name: "Total Allowances",
            sort: true,
            formatter: function (e) {
                return getAmountSalary(e);
            }
        },
        {
            id: "bonuss",
            name: "Total Bonus",
            sort: true,
            formatter: function (e) {
                return getAmountSalary(e);
            }
        },
        {
            id: "deductions",
            name: "Total Deduction",
            sort: true,
            formatter: function (e) {
                return getAmountSalary(e);
            }
        },
        {
            id: "TotalSalary",
            name: "Total Salary",
            sort: true,
            formatter: function (e, t) {
                var sideSalary =
                    getAmountSalary(t._cells[6].data)
                    + getAmountSalary(t._cells[7].data)
                    - getAmountSalary(t._cells[8].data);
                var totalSalary = t._cells[4].data * t._cells[5].data + sideSalary
                return totalSalary;
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
                    <li class="btnDetails" data-id=${t._cells[1].data} data-month=${t._cells[3].data.replace("/","%2F")}>
                        <a class="dropdown-item">
                        <i class="ri-eye-fill align-bottom me-2 text-muted"></i> View</a>
                    </li>
                    <li class="btnUpdate" data-id=${t._cells[1].data} data-month=${t._cells[3].data.replace("/", "%2F")}>
                        <a class="dropdown-item">
                        <i class="ri-pencil-fill align-bottom me-2 text-muted"></i> Edit</a>
                    </li>
                    <li class="dropdown-divider"></li>
                    <li class="btnDelete" data-id=${t._cells[1].data} data-month=${t._cells[3].data.replace("/", "%2F")}>
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
        var employeeId = $(this).data("id");
        var monthYear = $(this).data("month");
        console.log(`/Salary/ListSalary/${employeeId}/${monthYear}`)
        var selectedData = await getData(`/Salary/ListSalary/${employeeId}/${monthYear}`);
        setDataForm(selectedData);
        setTypeForm("details");
        setStatusForm(false);
        myModal.show()
    };

    //Add
    $(".btnAddNew").click(async function () {
        setDataForm(null);
        await loadSelectBox(true);
        setTypeForm("add");
        myModal.show();
    });
    $("#btnAdd").click(async function () {
        var dataInsert = getDataForm();
        if (!await isValidForm(dataInsert)) return;
        myModal.hide();
        await postData("/Salary/ListSalary", dataInsert);
        var listData = await getList("/Salary/ListSalary");
        newGrid.updateData(listData)
    });


    //Update
    async function updateInfor() {
        var employeeId = $(this).data("id");
        var monthYear = $(this).data("month");
        var selectedData = await getData(`/Salary/ListSalary/${employeeId}/${monthYear}`);
        setDataForm(selectedData);
        setTypeForm("edit");
        myModal.show()
    };
    $("#btnSave").click(async function () {
        var updatedData = getDataForm();
        console.log(updatedData)
        if (!await isValidForm(updatedData)) return;
        myModal.hide();
        await postData("/Salary/ListSalary", updatedData);
        var listData = await getList("/Salary/ListSalary");
        newGrid.updateData(listData)
    });


    //Remove
    async function deleteInfor() {
        var employeeId = $(this).data("id");
        var monthYear = $(this).data("month");
        localStorage.setItem("employeeId", employeeId)
        localStorage.setItem("monthYear", monthYear)
    };
    $("#delete-notification").click(async function () {
        var employeeId = localStorage.getItem("employeeId");
        var monthYear = localStorage.getItem("monthYear");
        await deleteData(`/Salary/ListSalary/Delete/${employeeId}/${monthYear}`);
        var listData = await getList("/Salary/ListSalary");
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
        var listData = await getList(`/Position/Search/${field}/${keyword}`);
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
                //return res.filter(x => x.isDeleted === false);
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

    async function getListByDeletedStatus(endPoint) {
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
                allowanceDetailsId = res.allowanceDetailsId;
                bonusDetailsId = res.bonusDetailsId;
                deductionDetailsId = res.deductionDetailsId;
                //console.log(allowanceDetailsId)
                //console.log(bonusDetailsId)
                //console.log(deductionDetailsId)
                return res;
            }
        } catch (e) {
            console.log(e);
            //AJAXCONFIG.ajaxFail(e);
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
        var salaryUpdate = [];
        var lstAllowances = choicesAllowances.getValue(true);
        var lstBonus = choicesBonus.getValue(true);
        var lstDeduction = choicesDeduction.getValue(true);

        var allowanceUpdate = [];
        for (var i = 0; i < lstAllowances.length; i++) {
            var allowanceId = lstAllowances[i];
            var obj = {
                id: (allowanceDetailsId != null && allowanceDetailsId.length-1>=i) ? allowanceDetailsId[i] : 0,
                employeeId: $("#employeeId").val(),
                allowanceId: allowanceId,
                startAt: new Date($("#monthYear").val())
            }
            allowanceUpdate.push(obj);
        }

        var bonusUpdate = [];
        for (var i = 0; i < lstBonus.length; i++) {
            var bonusId = lstBonus[i];
            var obj = {
                id: (bonusDetailsId != null && bonusDetailsId.length - 1 >= i) ? bonusDetailsId[i]:0,
                employeeId: $("#employeeId").val(),
                bonusId: bonusId,
                startAt: new Date($("#monthYear").val())
            }
            bonusUpdate.push(obj);
        }

        var deductionUpdate = [];
        for (var i = 0; i < lstDeduction.length; i++) {
            var deductionId = lstDeduction[i];
            var obj = {
                id: (deductionDetailsId != null && deductionDetailsId.length - 1 >= i)? deductionDetailsId[i]:0,
                employeeId: $("#employeeId").val(),
                deductionId: deductionId,
                startAt: new Date($("#monthYear").val())
            }
            deductionUpdate.push(obj);
        }

        return {
            employeeId: $("#employeeId").val(),
            basicSalary: $("#basicSalary").val(),
            coefficientSalary: $("#coefficientSalary").val(),
            allowances: allowanceUpdate,
            bonus: bonusUpdate,
            deduction: deductionUpdate,
            monthYear: new Date($("#monthYear").val())

        }
    }

    function setDataForm(data) {
        if (data == null) {
            $("#inforModal form :input").val("");
            choicesEmployee.setChoiceByValue("");
            choicesAllowances.setChoiceByValue([]);
            choicesBonus.setChoiceByValue([]);
            choicesDeduction.setChoiceByValue([]);
            return;
        }
        $("#employeeId").val(data.id);
        $("#basicSalary").val(data.basicSalary);
        $("#coefficientSalary").val(data.coefficientSalary);
        $("#monthYear").val(toMonthValueInput(data.monthYear));
        choicesEmployee.setChoiceByValue(data.id);
        var lstAllowancesId = data.allowances ? data.allowances.map(obj => obj.id) : [];
        var lstBonusId = data.bonuss?data.bonuss.map(obj => obj.id):[];
        var lstDeductionId = data.deductions ? data.deductions.map(obj => obj.id) : [];
        choicesAllowances.setChoiceByValue(lstAllowancesId);
        choicesBonus.setChoiceByValue(lstBonusId);
        choicesDeduction.setChoiceByValue(lstDeductionId);

    }

    function setStatusForm(status) {
        $("#inforModal form :input").prop("disabled", !status);
        $('input[name="id"]').prop("disabled", true);
        if (status == false) {
            choicesEmployee.disable();
            choicesAllowances.disable();
            choicesBonus.disable();
            choicesDeduction.disable();
        }
        else {
            choicesEmployee.enable();
            choicesAllowances.enable();
            choicesBonus.enable();
            choicesDeduction.enable();
        }
    }

    function setTypeForm(type) {
        $("#btnSave").hide();
        $("#btnAdd").hide();
        if (type == "add") {
            choicesEmployee.enable();
            $("#btnAdd").show();
        }
        else if (type == "edit") {
            choicesEmployee.disable();
            $("#btnSave").show();
        }

    }

    async function isValidForm(data) {
        var message = null;
        if (data.employeeId == "" || data.employeeId ==null) {
            message = "Employee is required!";
        }
        else if (data.monthYear == "" || data.monthYear == null) {
            message = "Month / Year is required!";
        }
        else if (data.basicSalary == "" || data.basicSalary == null) {
            message = "Basic Salary is required!";
        }
        else if (data.coefficientSalary == "" || data.coefficientSalary == null) {
            message = "Coefficient Salary is required!";
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

    function getAmountSalary(a) {
        if (a == null) return 0;
        var totalMoney = a.reduce((curr, obj) => curr + obj.amount, 0)
        return totalMoney;
    }
    async function loadSelectBox(isMultiReload) {
        if (!isMultiReload) {
            var lstEmployee = await getList("/Employee");
            var lstEmployeeChoices = [];
            for (var i = 0; i < lstEmployee.length; i++) {
                var obj = lstEmployee[i];
                lstEmployeeChoices.push({ value: obj.id, label: obj.fullName })
            }
            choicesEmployee.setChoices(lstEmployeeChoices);
        }
       
        var lstAllowance= await getListByDeletedStatus("/Allowance");
        var lstBonus = await getListByDeletedStatus("/Bonus");
        var lstDeduction = await getListByDeletedStatus("/Deduction");

        
        var lstAllowanceChoices = [];
        var lstBonusChoices = [];
        var lstDeductionChoices = [];

        
        for (var i = 0; i < lstAllowance.length; i++) {
            var obj = lstAllowance[i];
            lstAllowanceChoices.push({ value: obj.id, label: obj.name })
        }
        for (var i = 0; i < lstBonus.length; i++) {
            var obj = lstBonus[i];
            lstBonusChoices.push({ value: obj.id, label: obj.name })
        }
        for (var i = 0; i < lstDeduction.length; i++) {
            var obj = lstDeduction[i];
            lstDeductionChoices.push({ value: obj.id, label: obj.name })
        }
        choicesAllowances.clearStore();
        choicesBonus.clearStore();
        choicesDeduction.clearStore();
        choicesAllowances.setChoices(lstAllowanceChoices);
        choicesBonus.setChoices(lstBonusChoices);
        choicesDeduction.setChoices(lstDeductionChoices);
        
    }

    function toMonthValueInput(monthYear) {
        var parts = monthYear.split('/');
        var month = parts[0];
        var year = parts[1];
        //console.log(year + "-" + month)
        return year + "-" + month;
    }
});