import { htmlText, huGrid } from '../hugrid.js';
import { huApex, huCard } from '../huapex.js';
import * as API from '../api.js';
import * as AJAXCONFIG from '../ajax_config.js';

$(document).ready(function () {
    //CARD 
    var newCard = new huCard("rowCard");
    var cart1 = {
        title: "Total Employee",
        number: 10,
        unit:"people",
        url: "/Employee/PersonnelFiles",
        icon: "bx bx-user",
        iconColor:"success"
    }
    var cart2 = {
        title: "Total Bonus",
        number: 10000000,
        unit: "VND",
        url: "/Salary/BonusList",
        icon: "bx bx-dollar",
        iconColor: "warning"
    }
    newCard.add(cart1)
    newCard.add(cart2)

    //TABLE
    var listAllData = [{
        id: 1,
        product: "Test M",
        price: "215.00",
        rating: "4.2",
    }, {
        id: 2,
        product: "Test A",
        price: "160.00",
        rating: "4.3",
    }, {
        id: 3,
        product: "Test B",
        price: "125.00",
        rating: "4.5",
    }, {
        id: 4,
        product: "Test C",
        price: "340.00",
        rating: "4.2",
    }, {
        id: 5,
        product: "Test D",
        price: "175.00",
        rating: "4.4",
    }];
    var columns = [
        {
            id: "id",
            name: htmlText(`<div class="text-center">Id</div>`),
            formatter: function (e) {
                return htmlText(`<div class="text-center">${e}</div>`)
            }
        },
        {
            id: "product",
            name: "Product"
        },
        {
            name: "Price",
            formatter: function (e) {
                return htmlText("$" + e)
            }
        }, {
            name: "Rating",
            formatter: function (e) {
                return htmlText('<span class="badge bg-light text-body fs-12 fw-medium"><i class="mdi mdi-star text-warning me-1"></i>' + e + "</span></td>")
            }
        }
    ];
    var newGrid = new huGrid("table-product-list-all", columns, listAllData);


    //CHART
    var dataObj = {
        cate1: 30,
        cate2: 40,
        cate3: 50,
        cate4: 60
    }

    var chartLabels = ["Cate 1", "Cate 2", "Cate 3", "Cate 4"];
    var newChart = new huApex("apex-barchar", chartLabels, dataObj);
    newChart.barChart();

    var chartLabels2 = ["Cate 1", "Cate 2", "Cate 3", "Cate 4"];
    var newChart2 = new huApex("apex-piechart", chartLabels2, dataObj);
    newChart2.pieChart();

    var chartLabels3 = ["Cate 1", "Cate 2", "Cate 3", "Cate 4"];
    var newChart3 = new huApex("apex-donutchart", chartLabels3, dataObj);
    newChart3.donutChart();
});