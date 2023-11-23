import { htmlText, huGrid } from '../hugrid.js';
import { huApex, huCard } from '../huapex.js';
import * as API from '../api.js';
import * as AJAXCONFIG from '../ajax_config.js';

$(document).ready(async function () {
    //ROW 1: CARD 
    var newCardEmployee = new huCard("rowCard");
    var totalEmployee = await getData("/Employee/Statistic/Total");
    var cardEmployee1 = {
        title: "Total Employee",
        number: totalEmployee,
        unit: "Employee",
        url: "/Employee/PersonnelFiles",
        icon: "bx bx-user",
        iconColor: "warning"
    }
    var totalDepartment = await getData("/Department/Statistic/Total");
    var cardEmployee2 = {
        title: "Total Department",
        number: totalDepartment,
        unit: "Department",
        url: "/Employee/Department",
        icon: "bx bx-user",
        iconColor: "success"
    }
    var totalPosition = await getData("/Position/Statistic/Total");
    var cardEmployee3 = {
        title: "Total Position",
        number: totalPosition,
        unit: "Position",
        url: "/Employee/Department",
        icon: "bx bx-user",
        iconColor: "info"
    }

    var totalContract = await getData("/Contract/Statistic/Total");
    var cardEmployee4 = {
        title: "Total Contract",
        number: totalContract,
        unit: "Contract",
        url: "/Employee/Contract",
        icon: "bx bxs-contact",
        iconColor: "danger"
    }

    newCardEmployee.add(cardEmployee1)
    newCardEmployee.add(cardEmployee2)
    newCardEmployee.add(cardEmployee3)
    newCardEmployee.add(cardEmployee4)


    //ROW 2: TABLE + BARCHAR
    var listDataEmployee = await getList("/Employee/Statistic/TopYoungest/12");
    var columnsEmployee  = [
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
            name: "Full Name"
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
        }];
    var newGridEmployee = new huGrid("table-employee", columnsEmployee, listDataEmployee);

    var employeeCountByDepartment = await getData("/Department/Statistic/EmployeeCount");

    // Chart: Display employee count statistics using a bar chart
    var chartDepartmentLabels = Object.keys(employeeCountByDepartment);
    var chartDepartmentData = Object.values(employeeCountByDepartment);

    var newDepartmentChart = new huApex("barchar-Department", chartDepartmentLabels, chartDepartmentData);
    newDepartmentChart.barChart();


    //ROW 3: CHART
    var dataObjGender = await getData("/Employee/Statistic/MaleFemale");
    var chartLabelsGender = ["Male", "Female"];
    var newChartGender = new huApex("apex-piechart", chartLabelsGender, dataObjGender);
    newChartGender.pieChart();

    var dataObjAllowance = await getData("/Allowance/Statistic/TotalAllowance");
    var dataObjBonus = await getData("/Bonus/Statistic/TotalBonus");
    var dataObjDeduction = await getData("/Deduction/Statistic/TotalDeduction");
    var dataSalary = { dataObjAllowance, dataObjBonus, dataObjDeduction }
    //Bar chart
    var chartLabelsSalary = ["Allowance", "Bonus", "Deduction"];
    var newChartSalary = new huApex("apex-barchar-salary", chartLabelsSalary, dataSalary);
    newChartSalary.barChart();

    //ROW 4:
    var newCardSalary = new huCard("rowCard-salary");
    var AllowanceTotal = await getData("/Allowance/Statistic/Month");
    var cardAllowanceTotal = {
        title: "Allowance total in month",
        number: AllowanceTotal,
        unit: "VND",
        url: "/Salary/AllowanceList",
        icon: "bx bx-dollar",
        iconColor: "warning"
    }
    var BonusTotal = await getData("/Bonus/Statistic/Month");
    var cardBonus = {
        title: "Bonus total in month",
        number: BonusTotal,
        unit: "VND",
        url: "/Salary/BonusList",
        icon: "bx bx-dollar",
        iconColor: "warning"
    }
    var DeductionTotal = await getData("/Deduction/Statistic/11");
    var cardDeduction = {
        title: "Deduction total in month",
        number: DeductionTotal,
        unit: "VND",
        url: "/Salary/DeductionList",
        icon: "bx bx-dollar",
        iconColor: "warning"
    }
    var totalDeduction2 = await getData("/Deduction/Statistic/GetTotalAmountDeduction");
    var cardDeduction2 = {
        title: "Total Amount Deduction",
        number: totalDeduction2,
        unit: "VND",
        url: "/Salary/DeductionList",
        icon: "bx bx-dollar",
        iconColor: "warning"
    }

    newCardSalary.add(cardAllowanceTotal);
    newCardSalary.add(cardBonus);
    newCardSalary.add(cardDeduction);
    newCardSalary.add(cardDeduction2);


    //ROW 5
    var listDataTime = await getList("/TimeKeeping/Statistic/GetUsuallyLate/5");
    var columnsTime = [
        {
            id: "timesName",
            name: "Employee Name"
        },
        {
            id: "timesCount",
            name: "Times"
        }];
    var newGridTime = new huGrid("list5Late", columnsTime, listDataTime);

    var dataTime = await getData("/TimeKeeping/Statistic/GetListOnTime/46");

    var chartTime = ["monday", "tuesday", "wednesday", "thursday", "friday", "saturday", "sunday"];
    var newChartTime = new huApex("DetailChartTimeKeeping", chartTime, dataTime);
    newChartTime.barChart();


    //ROW 6
    var newCardTask = new huCard("rowCard-task");
    var totalProject = await getData("/Project/Statistic/Total");
    var totalProjectTime = await getData("/Project/Statistic/TotalProjectTime");
    var cardproject1 = {
        title: "Total Project",
        number: totalProject,
        unit: "project",
        url: "/Tasks/Project",
        icon: "bx bxs-bar-chart-square",
        iconColor: "success"
    }
    var cardproject2 = {

        title: "Total Days of Project",
        number: totalProjectTime,
        unit: "days",
        url: "/Tasks/Project",
        icon: "bx bx-time",
        iconColor: "warning"
    }
    var totalTask = await getData("/Task/Statistic/Total");
    var totalTaskTime = await getData("/Task/Statistic/TotalTaskTime");
    var cardtask1 = {
        title: "Total Task",
        number: totalTask,
        unit: "task",
        url: "/Tasks/TaskList",
        icon: "bx bx-task",
        iconColor: "success"
    }
    var cardtask2 = {
        title: "Total Time",
        number: totalTaskTime,
        unit: "hours",
        url: "/Tasks/TaskList",
        icon: "bx bx-time",
        iconColor: "warning"
    }
    newCardTask.add(cardproject1)
    newCardTask.add(cardproject2)
    newCardTask.add(cardtask1)
    newCardTask.add(cardtask2)

    //ROW 7: CHART + TABLE
    var dataObjTask = await getData("/Task/Statistic/Task");
    var chartLabelsTask = ["NotStarted", "Progressing", "Finished"];
    var newChartTask = new huApex("apex-piechart-task", chartLabelsTask, dataObjTask);
    newChartTask.pieChart();

    var listDataTask = await getList("/Task/Statistic/TopEarliest/3");
    var columnsTask  = [
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
            id: "status",
            name: "Status"
        },

        {
            id: "startTime",
            name: "StartTime",
            formatter: function (e) {
                return toDateTime(e)
            }
        },
        {
            id: "content",
            name: "Content"
        },
        {
            id: "description",
            name: "Description"
        }
    ];
    var newGridTask = new huGrid("table-task", columnsTask, listDataTask);


    //AJAX
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
            //AJAXCONFIG.ajaxFail(e);
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


    //format dateTime
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
});