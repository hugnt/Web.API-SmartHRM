import '../assets/libs/apexcharts/apexcharts.min.js'

export class huApex {
    #chartId;
    #labels;
    #data;
    constructor(chartId, labels, dataObj) {
        this.#chartId = chartId;
        this.#labels = labels;
        this.#data = Object.values(dataObj);
    }

    barChart() {
        var optionsBarchart = {
            chart: {
                type: 'bar'
            },
            series: [{
                data: this.#data
            }],
            plotOptions: {
                bar: {
                    columnWidth: '50%',
                    distributed: true,
                }
            },
            xaxis: {
                categories: this.#labels
            }
        };
        var barChart = new ApexCharts(document.querySelector(`#${this.#chartId}`), optionsBarchart);
        barChart.render();
    }

    pieChart() {
        var optionsPiechart = {
            series: this.#data,
            chart: { height: 300, type: "pie" },
            labels: this.#labels,
            legend: { position: "bottom" },
            dataLabels: { dropShadow: { enabled: !1 } },
            colors: this.#getChartColorsArray(this.#chartId)
        };
        var pieChart = new ApexCharts(document.querySelector(`#${this.#chartId}`), optionsPiechart);
        pieChart.render();
    }

    donutChart() {
        var optionsDonutChart = {
            series: this.#data,
            chart: { height: 300, type: "donut" },
            labels: this.#labels,
            legend: { position: "bottom" },
            dataLabels: { dropShadow: { enabled: !1 } },
            colors: this.#getChartColorsArray(this.#chartId)
        };
        var donutChart = new ApexCharts(document.querySelector(`#${this.#chartId}`), optionsDonutChart);
        donutChart.render();
    }

    #getChartColorsArray(e) {
        if (null !== document.getElementById(e))
        return (
            (e = document.getElementById(e).getAttribute("data-colors")),
            (e = JSON.parse(e)).map(function (e) {
                var t = e.replace(" ", "");
                return -1 === t.indexOf(",")
                    ? getComputedStyle(document.documentElement).getPropertyValue(t) || t
                    : 2 == (e = e.split(",")).length
                        ? "rgba(" +
                        getComputedStyle(document.documentElement).getPropertyValue(e[0]) +
                        "," +
                        e[1] +
                        ")"
                        : t;
            })
        );
    }


}

export class huCard {
    #htmlCard;
    #rowId;
    #lstDataObj;
    constructor(rowId) {
        this.#rowId = rowId;
        this.#lstDataObj = [];
        document.querySelector(`#${this.#rowId}`).innerHTML = "";
    }
    add(dataObj) {
        document.querySelector(`#${this.#rowId}`).innerHTML = "";
        var htmlCardRow = "";
        this.#lstDataObj.push(dataObj);
        for (var i = 0; i < this.#lstDataObj.length; i++) {
            var item = this.#lstDataObj[i];
            htmlCardRow += `<div class="col-xl-3 col-md-6">
                <div class="card card-animate">
                    <div class="card-body">
                        <div class="d-flex align-items-center">
                            <div class="flex-grow-1 overflow-hidden">
                                <p class="text-uppercase fw-medium text-muted text-truncate mb-0">${item.title}</p>
                            </div>
                        </div>
                        <div class="d-flex align-items-end justify-content-between mt-4">
                            <div>
                                <h4 class="fs-22 fw-semibold ff-secondary mb-4"><span class="counter-value" data-target="${item.number}">${item.number}</span>${" "+item.unit}</h4>
                                <a href="${item.url}" class="text-decoration-underline">View details</a>
                            </div>
                            <div class="avatar-sm flex-shrink-0">
                                <span class="avatar-title bg-${item.iconColor}-subtle rounded fs-3">
                                    <i class="${item.icon} text-${item.iconColor}"></i>
                                </span>
                            </div>
                        </div>
                    </div>
                </div>
            </div>`;
        }
        document.querySelector(`#${this.#rowId}`).innerHTML = htmlCardRow;
    }

}