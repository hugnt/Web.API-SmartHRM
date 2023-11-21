import { htmlText, huGrid } from '../hugrid.js';
import { huApex, huCard } from '../huapex.js';
import * as API from '../api.js';
import * as AJAXCONFIG from '../ajax_config.js';

$(document).ready(async function () {
    //CARD 
    var newCard = new huCard("rowCard-allowance");
    var AllowanceQuantity = await getData("/Allowance/Statistic/TotalAllowance");
    var AllowanceTotal = await getData("/Allowance/Statistic/Month");
    var card1 = {
        title: "Allowance Quantity",
        number: AllowanceQuantity,
        unit: "allowance",
        url: "/Salary/AllowanceList",
        icon: "bx bx-user",
        iconColor: "success"
    }
    var card2 = {
        title: "Allowance total in month",
        number: AllowanceTotal,
        unit: "VND",
        url: "/Salary/AllowanceList",
        icon: "bx bx-dollar",
        iconColor: "warning"
    }
    newCard.add(card1)
    newCard.add(card2)

    //TABLE
    var listData2 = await getList("/Allowance/Statistic/TopAllowanceHighest/8");
    var columns2 = [
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
            name: "Allowance Name",
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
    ];
        /*{
            id: "gender",
            name: "Gender",
            formatter: function (e) {
                return e ? "Male" : "Female";
            }
        }*/
    var newGrid2 = new huGrid("table-product-list-all-allowance", columns2, listData2);


    //CHART
    /*var dataObj = await getData("/Employee/Statistic/MaleFemale");

    var chartLabels = ["Male", "Female"];
    var newChart = new huApex("apex-barchar", chartLabels, dataObj);
    newChart.barChart();

    var chartLabels2 = ["Male", "Female"];
    var newChart2 = new huApex("apex-piechart", chartLabels2, dataObj);
    newChart2.pieChart();

    var chartLabels3 = ["Male", "Female"];
    var newChart3 = new huApex("apex-donutchart", chartLabels3, dataObj);
    newChart3.donutChart();*/

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
});