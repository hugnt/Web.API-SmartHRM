import { htmlText, huGrid } from '../hugrid.js';
import { huApex, huCard } from '../huapex.js';
import * as API from '../api.js';
import * as AJAXCONFIG from '../ajax_config.js';

$(document).ready(async function () {
    //CARD 
    var newCard = new huCard("rowCard-bonus");
    var BonusQuantity = await getData("/Bonus/Statistic/TotalBonus");
    var BonusTotal = await getData("/Bonus/Statistic/Month");
    var card1 = {
        title: "Bonus Quantity",
        number: BonusQuantity,
        unit: "allowance",
        url: "/Salary/BonusList",
        icon: "bx bx-user",
        iconColor: "success"
    }
    var card2 = {
        title: "Bonus total in month",
        number: BonusTotal,
        unit: "VND",
        url: "/Salary/BonusList",
        icon: "bx bx-dollar",
        iconColor: "warning"
    }
    newCard.add(card1)
    newCard.add(card2)

    //TABLE
    var listData3 = await getList("/Bonus/Statistic/TopBonusHighest/8");
    var columns3 = [
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
    ];
        /*{
            id: "gender",
            name: "Gender",
            formatter: function (e) {
                return e ? "Male" : "Female";
            }
        }*/
    var newGrid2 = new huGrid("table-product-list-all-bonus", columns3, listData3);


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