import {htmlText, huGrid} from './hugrid.js';

var productListAllData = [{
    id: 1,
    product: {
        img: "img-1.png",
        title: "Half Sleeve Round Neck T-Shirts",
        category: "Fashion"
    },
    stock: "12",
    price: "215.00",
    orders: "48",
    rating: "4.2",
    published: {
        publishDate: "12 Oct, 2021",
        publishTime: "10:05 AM"
    }
}, {
    id: 2,
    product: {
        img: "img-2.png",
        title: "Urban Ladder Pashe Chair",
        category: "Furniture"
    },
    stock: "06",
    price: "160.00",
    orders: "30",
    rating: "4.3",
    published: {
        publishDate: "06 Jan, 2021",
        publishTime: "01:31 PM"
    }
}, {
    id: 3,
    product: {
        img: "img-3.png",
        title: "350 ml Glass Grocery Container",
        category: "Grocery"
    },
    stock: "10",
    price: "125.00",
    orders: "48",
    rating: "4.5",
    published: {
        publishDate: "26 Mar, 2021",
        publishTime: "11:40 AM"
    }
}, {
    id: 4,
    product: {
        img: "img-4.png",
        title: "Fabric Dual Tone Living Room Chair",
        category: "Furniture"
    },
    stock: "15",
    price: "340.00",
    orders: "40",
    rating: "4.2",
    published: {
        publishDate: "19 Apr, 2021",
        publishTime: "02:51 PM"
    }
}, {
    id: 5,
    product: {
        img: "img-5.png",
        title: "Crux Motorsports Helmet",
        category: "Automotive Accessories"
    },
    stock: "08",
    price: "175.00",
    orders: "55",
    rating: "4.4",
    published: {
        publishDate: "30 Mar, 2021",
        publishTime: "09:42 AM"
    }
}, {
    id: 6,
    product: {
        img: "img-6.png",
        title: "Half Sleeve T-Shirts (Blue)",
        category: "Fashion"
    },
    stock: "15",
    price: "225.00",
    orders: "48",
    rating: "4.2",
    published: {
        publishDate: "12 Oct, 2021",
        publishTime: "04:55 PM"
    }
}, {
    id: 7,
    product: {
        img: "img-7.png",
        title: "Noise Evolve Smartwatch",
        category: "Watches"
    },
    stock: "12",
    price: "105.00",
    orders: "45",
    rating: "4.3",
    published: {
        publishDate: "15 May, 2021",
        publishTime: "03:40 PM"
    }
}, {
    id: 8,
    product: {
        img: "img-8.png",
        title: "Sweatshirt for Men (Pink)",
        category: "Fashion"
    },
    stock: "20",
    price: "120.00",
    orders: "48",
    rating: "4.2",
    published: {
        publishDate: "21 Jun, 2021",
        publishTime: "12:18 PM"
    }
}, {
    id: 9,
    product: {
        img: "img-8.png",
        title: "Reusable Ecological Coffee Cup",
        category: "Grocery"
    },
    stock: "14",
    price: "325.00",
    orders: "55",
    rating: "4.3",
    published: {
        publishDate: "15 Jan, 2021",
        publishTime: "10:29 PM"
    }
}, {
    id: 10,
    product: {
        img: "img-10.png",
        title: "Travel Carrying Pouch Bag",
        category: "Kids"
    },
    stock: "20",
    price: "180.00",
    orders: "60",
    rating: "4.3",
    published: {
        publishDate: "15 Jun, 2021",
        publishTime: "03:51 PM"
    }
}, {
    id: 11,
    product: {
        img: "img-1.png",
        title: "Half Sleeve Round Neck T-Shirts",
        category: "Fashion"
    },
    stock: "12",
    price: "215.00",
    orders: "48",
    rating: "4.2",
    published: {
        publishDate: "12 Oct, 2021",
        publishTime: "10:05 AM"
    }
}, {
    id: 12,
    product: {
        img: "img-2.png",
        title: "Urban Ladder Pashe Chair",
        category: "Furniture"
    },
    stock: "06",
    price: "160.00",
    orders: "30",
    rating: "4.3",
    published: {
        publishDate: "06 Jan, 2021",
        publishTime: "01:31 PM"
    }
}];

var productListNew = [{
    id: 99,
    product: {
        img: "img-1.png",
        title: "Half Sleeve Round Neck T-Shirts",
        category: "Fashion"
    },
    stock: "12",
    price: "215.00",
    orders: "48",
    rating: "4.2",
    published: {
        publishDate: "12 Oct, 2021",
        publishTime: "10:05 AM"
    }
}, {
    id: 21,
    product: {
        img: "img-2.png",
        title: "Urban Ladder Pashe Chair",
        category: "Furniture"
    },
    stock: "06",
    price: "160.00",
    orders: "30",
    rating: "4.3",
    published: {
        publishDate: "06 Jan, 2021",
        publishTime: "01:31 PM"
    }
}, {
    id: 13,
    product: {
        img: "img-3.png",
        title: "350 ml Glass Grocery Container",
        category: "Grocery"
    },
    stock: "10",
    price: "125.00",
    orders: "48",
    rating: "4.5",
    published: {
        publishDate: "26 Mar, 2021",
        publishTime: "11:40 AM"
    }
}, {
    id: 24,
    product: {
        img: "img-4.png",
        title: "Fabric Dual Tone Living Room Chair",
        category: "Furniture"
    },
    stock: "15",
    price: "340.00",
    orders: "40",
    rating: "4.2",
    published: {
        publishDate: "19 Apr, 2021",
        publishTime: "02:51 PM"
    }
}, {
    id: 125,
    product: {
        img: "img-5.png",
        title: "Crux Motorsports Helmet",
        category: "Automotive Accessories"
    },
    stock: "08",
    price: "175.00",
    orders: "55",
    rating: "4.4",
    published: {
        publishDate: "30 Mar, 2021",
        publishTime: "09:42 AM"
    }
}];
var columns= [
    {
        id: "id",
        name:  htmlText(`<div class="text-center">Id</div>`),
        sort:false,
        formatter: function(e) {
            return htmlText(`<div class="text-center">${e}</div>`)
        }
    }, 
    {
        name: "Product",
        sort: true,
        data: function(e) {
            return htmlText(`
                <div class= "d-flex align-items-center" >
                    <div class="flex-shrink-0 me-3">
                        <div class="avatar-sm bg-light rounded p-1">
                            <img src="../assets/images/products/${e.product.img}" alt="" class="img-fluid d-block">
                        </div >
                    </div>
                    <div class="flex-grow-1">
                        <h5 class="fs-14 mb-1">
                            <a href="apps-ecommerce-product-details.html" class="text-body">${e.product.title}</a>
                        </h5>
                        <p class="text-muted mb-0">Category : <span class="fw-medium">${e.product.category}</span></p>
                    </div >
                </div > `)
        }
    }, {
        name: "Stock",
    }, {
        name: "Price",
        sort: true,
        formatter: function(e) {
            return htmlText("$" + e)
        }
    }, {
        name: "Orders",
    }, {
        name: "Rating",
        formatter: function(e) {
            return htmlText('<span class="badge bg-light text-body fs-12 fw-medium"><i class="mdi mdi-star text-warning me-1"></i>' + e + "</span></td>")
        }
    }, {
        name: "Published",
        data: function(e) {
            return htmlText(e.published.publishDate + '<small class="text-muted ms-1">' + e.published.publishTime + "</small>")
        }
    }, {
        id: 'Action',
        name: htmlText('<div class="text-center">Action</div>'),
        sort:false,
        formatter: function(e, t) {
            //t = (new DOMParser).parseFromString(t._cells[0].data.props.content, "text/html").body.querySelector(".checkbox-product-list .form-check-input").value;
            return htmlText(`
            <div class="dropdown text-center">
                <button class="btn btn-soft-secondary btn-sm dropdown" type="button" data-bs-toggle="dropdown" aria-expanded="false"><i class="ri-more-fill"></i></button>
                <ul class="dropdown-menu dropdown-menu-end">
                    <li>
                        <a class="dropdown-item" data-id=${t._cells[1].data}>
                        <i class="ri-eye-fill align-bottom me-2 text-muted"></i> View</a>
                    </li>
                    <li>
                        <a class="dropdown-item" data-id=${t._cells[1].data}>
                        <i class="ri-pencil-fill align-bottom me-2 text-muted"></i> Edit</a>
                    </li>
                    <li class="dropdown-divider"></li>
                    <li>
                        <a class="dropdown-item " href="#" data-id=${t._cells[1].data} data-bs-toggle="modal" data-bs-target="#removeItemModal">
                            <i class="ri-delete-bin-fill align-bottom me-2 text-muted"></i> Delete</a>
                    </li>
                </ul>
            </div>`)
        }
}];

// var newGrid = hugrid("table-product-list-all", columns, productListAllData);
var newGrid = new huGrid("table-product-list-all", columns, productListAllData);
$(".btnAddNew").click(function(){
    newGrid.updateData(productListNew);
});

$(".btn-info").click(function(){
    console.log(newGrid.getListSelectedId())
})