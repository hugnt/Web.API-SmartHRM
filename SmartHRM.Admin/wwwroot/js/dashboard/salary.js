import { htmlText, huGrid } from '../hugrid.js';
import { huApex, huCard } from '../huapex.js';
import * as API from '../api.js';
import * as AJAXCONFIG from '../ajax_config.js';

$(document).ready(async function () {
    //CARD
/*    var newCard = new huCard("rowCard4");
    var DeductionQuantity = await getData("/Deduction/Statistic/TotalDeduction");
    var DeductionTotal = await getData("/Deduction/Statistic/11");
    var card1 = {
        title: "Deduction Quantity",
        number: DeductionQuantity,
        unit: "deduction",
        url: "/Salary/DeductionList",
        icon: "bx bx-user",
        iconColor: "success"
    }
    var card2 = {
        title: "Deduction total in month",
        number: DeductionTotal,
        unit: "VND",
        url: "/Salary/DeductionList",
        icon: "bx bx-dollar",
        iconColor: "warning"
    }
    newCard.add(card1)
    newCard.add(card2)*/

    //TABLE
/*    var listData4 = await getList("/Deduction/Statistic/TopDeductionHighest/8");
    var columns4 = [
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
            name: "Bonus Name",
            sort: true
        },
        {
            id: "amount",
            name: "Amount",
            sort: true,
            formatter: function (e) {
                return e == null ? "0 VND" : e + " VND";
            }
        },
        {
            id: "note",
            name: "Note"
        },
    ];*/
        /*{
            id: "gender",
            name: "Gender",
            formatter: function (e) {
                return e ? "Male" : "Female";
            }
        }*/
/*    var newGrid2 = new huGrid("table-product-list-all4", columns4, listData4);*/


    //CHART
    //Total
    var dataObj1 = await getData("/Allowance/Statistic/TotalAllowance");
    var dataObj2 = await getData("/Bonus/Statistic/TotalBonus");
    var dataObj3 = await getData("/Deduction/Statistic/TotalDeduction");
    var data1 = { dataObj1, dataObj2, dataObj3 }
    //Bar chart
    var chartLabels1 = ["Allowance", "Bonus", "Deduction"];
    var newChart = new huApex("apex-barchar-salary", chartLabels1, data1);
    newChart.barChart();
    //Pie chart
    var chartLabels2 = ["Allowance", "Bonus", "Deduction"];
    var newChart2 = new huApex("apex-piechart-salary", chartLabels2, data1);
    newChart2.pieChart();
    //Donut chart
    var chartLabels3 = ["Allowance", "Bonus", "Deduction"];
    var newChart3 = new huApex("apex-donutchart-salary", chartLabels3, data1);
    newChart3.donutChart();

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
/*            AJAXCONFIG.ajaxFail(e);
*/        }
        finally {
            AJAXCONFIG.ajaxAfterSend();
        }
    }
});