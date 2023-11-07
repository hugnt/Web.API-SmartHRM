import {Grid, html, PluginPosition } from '../assets/libs/gridjs/grid.module.js';
import {RowSelection} from '../assets/libs/gridjs/selection/selection.module.js';

export class huGrid{
    #grid;
    #config;
    #data;
    constructor(tableId, columnsConfig, data){
        this.#data = data;
        this.#config = {
            columns: [
                {
                    id: 'checkBox',
                    name: html(` <div class="text-center">
                                            <input class="form-check-input" type="checkbox" id="checkAll" value="option">
                                        </div>`),
                    sort:false,
                    plugin: {
                        component: RowSelection
                    },
                    hidden: true,
                    data: () => false
                }, 
                ...columnsConfig
            ],
            className: {
                th: "text-muted"
            },
            fixedHeader: true,
            pagination: {
                limit: 8,
                resetPageOnUpdate: true
            },
            height: 'auto',
            style: {
                container : {
                  
                },
                table: {
                  
                },
                header : {
                    
                },
                footer  : {
                  display:'flex',
                  alignItems:'center',
                  flexDirection: 'row-reverse',
                  justifyContent: 'space-between'
                }
              },
            autoWidth: true,
            // data: productListAllData,
            data: () => {
                return new Promise(resolve => {
                  setTimeout(() =>
                    resolve(data), 2000);
                });
              }
        };
        this.#grid = new Grid(this.#config);
        this.#grid.render(document.getElementById(tableId));
        this.#initEventListener(this.#grid, this.#config);
        console.log(this.#grid);
    }

    updateData(newData){
        this.#data = newData;
        
        this.#grid.config.style.footer.flexDirection ='unset'
        this.#grid.config.plugin.remove('pagination');
        this.#grid.config.plugin.remove('checkBox');
        this.#grid.config.columns[0].hidden = true;
        this.#grid.config.columns[0].data = () => false;
        this.#grid.config.columns[0].name = html(` <div class="text-center">
            <input class="form-check-input" type="checkbox" id="checkAll" value="option"} >
        </div>`);
        $(".btnCheckTool").hide();
        this.#grid.updateConfig({
            data: newData
        }).forceRender();
        $("#pageSize").val(this.#grid.config.pagination.limit);
    }

    getListSelectedId(){
        var res = [];
        if(this.#grid.config.store.state.rowSelection){
            var state = this.#grid.config.store.state;
            for(var i = 0; i<state.rowSelection.rowIds.length; i++){
                var selectedRowId = state.rowSelection.rowIds[i];
                res.push(state.data._rows.find(x => x._id == selectedRowId)._cells[1].data);
            }
        }
        return res;
    }

    #updateConfigTable(config){
        config.style.footer.flexDirection ='unset'
        this.#grid.config.plugin.remove('pagination');
        this.#grid.config.plugin.remove('checkBox');
        this.#grid.updateConfig({
            columns: config.columns,
            data: this.#data
       }).forceRender();
       $("#pageSize").val(this.#grid.config.pagination.limit);
    }

    #initEventListener(grid, config) {
        const self = this;
        $(".btnCheckTool").hide()
        $("#btnOpenCheck").click(function(){
            config.columns[0].hidden = !grid.config.columns[0].hidden;
            config.columns[0].data = () => false;
            config.columns[0].name = html(` <div class="text-center">
                <input class="form-check-input" type="checkbox" id="checkAll" value="option"} >
            </div>`);
            $(".btnCheckTool").hide();
            self.#updateConfigTable(config);   
            console.log(grid)
        });
    
        //add Listeners
        grid.config.store.subscribe(function (state) {
            $("#checkAll").change(function(){
                if(state.rowSelection!=null){
                    state.rowSelection = null;
                    state.data = null;
                }
                config.columns[0].data = () => this.checked;
                config.columns[0].name = html(` <div class="text-center">
                    <input class="form-check-input" type="checkbox" id="checkAll" value="option" ${this.checked&&"checked"} >
                </div>`);
                self.#updateConfigTable(config);
        
            });  
        });
        grid.config.store.subscribe(function (state) {
            if(!grid.config.columns[0].hidden&&state.rowSelection!=null&&state.rowSelection.rowIds!=null)
            { 
                $(".btnCheckTool").show();
            }
            else{
                if(state.rowSelection!=null){
                    state.rowSelection=null;
                }
                $(".btnCheckTool").hide();
            
                
            }
            //console.log("SELECTED ROWS: "+state.rowSelection)
        });

        grid.config.store.subscribe(function (state) {
            $("#pageSize").change(function(){
                console.log($(this).val())
                config.pagination.limit = $(this).val();
                if(state.rowSelection!=null){
                    state.rowSelection = null;
                    state.data = null;
                }
                
                self.#updateConfigTable(config);
            });  
           
        });
        grid.config.store.subscribe(function (state) {
            console.log('checkbox updated', state.rowSelection);
            if(state.rowSelection){
                for(var i = 0; i<state.rowSelection.rowIds.length; i++){
                    var selecyedId = state.rowSelection.rowIds[i];
                    console.log('selected id', state.data._rows.find(x => x._id == selecyedId)._cells[1].data);
                }
            }
            
           
          })
    
    
        //add Plugins
        function selectShowBoxPlugin() {
            $(".gridjs-pagination").css("width","60%");
            return html(
            `<div class="d-flex align-items-center mt-1" id="selectShowBox">
                <span >Shows</span>
                <select class="form-control mx-1 py-1" id="pageSize" style="width:5rem">
                    <option value="8" selected>8</option>
                    <option value="25">25</option>
                    <option value="50">50</option>
                </select>
                <span>entries</span>
            </div>`)
            
        }
        grid.plugin.add({
            id: 'selectShowBoxPlugin',
            component: selectShowBoxPlugin,
            position: PluginPosition.Footer,
        });
        
    };

}

export function htmlText(htmlText){
    return html(htmlText);
}



//TESTTTTT
function hugrid(tableId, columnsConfig, data){
    var config = {
        columns: [
            {
                id: 'checkBox',
                name: html(` <div class="text-center">
                                        <input class="form-check-input" type="checkbox" id="checkAll" value="option">
                                    </div>`),
                sort:false,
                plugin: {
                    component: RowSelection
                },
                hidden: true,
                data: () => false
            }, 
            ...columnsConfig
        ],
        className: {
            th: "text-muted"
        },
        fixedHeader: true,
        pagination: {
            limit: 8,
            resetPageOnUpdate: true
        },
        height: 'auto',
        style: {
            container : {
              
            },
            table: {
              
            },
            header : {
                
            },
            footer  : {
              display:'flex',
              alignItems:'center',
              flexDirection: 'row-reverse',
              justifyContent: 'space-between'
            }
          },
        autoWidth: true,
        // data: productListAllData,
        data: () => {
            return new Promise(resolve => {
              setTimeout(() =>
                resolve(data), 2000);
            });
          }
    };
    const grid = new Grid(config);
    grid.render(document.getElementById(tableId));
    configTheme(grid, config, data);
    console.log(grid);
}

function configTheme(grid, config, data) {
    $(".btnCheckTool").hide()
    $("#btnOpenCheck").click(function(){
        config.columns[0].hidden = !grid.config.columns[0].hidden;
        config.columns[0].data = () => false;
        config.columns[0].name = html(` <div class="text-center">
            <input class="form-check-input" type="checkbox" id="checkAll" value="option"} >
        </div>`);
        $(".btnCheckTool").hide();
        updateConfigTable(config);   
    });

    function updateConfigTable(config){
        config.style.footer.flexDirection ='unset'
        grid.config.plugin.remove('pagination');
        grid.config.plugin.remove('checkBox');
       // grid.config.plugin.plugins.forEach((p) => {grid.config.plugin.remove(p.id);});
        grid.updateConfig({
            columns: config.columns,
            data: data
       }).forceRender();
       //console.log(grid.config.store.getListeners())
    }

    //add Listeners
    grid.config.store.subscribe(function (state) {
        $("#checkAll").change(function(){
            state.data = null;
            state.rowSelection = null;
            config.columns[0].data = () => this.checked;
            config.columns[0].name = html(` <div class="text-center">
                <input class="form-check-input" type="checkbox" id="checkAll" value="option" ${this.checked&&"checked"} >
            </div>`);
            updateConfigTable(config);
    
        });  
    });
    grid.config.store.subscribe(function (state) {
        if(!grid.config.columns[0].hidden&&state.rowSelection&&state.rowSelection.rowIds.length > 0)
        {
            $(".btnCheckTool").show();
        }
        else{
            //state.data = null;
            state.rowSelection = null;
            $(".btnCheckTool").hide();
        }
        //console.log("SELECTED ROWS: "+state.rowSelection)
    });
    grid.config.store.subscribe(function (state) {
        $("#pageSize").change(function(){
            console.log($(this).val())
            config.pagination.limit = $(this).val();
            
            updateConfigTable(config);
        });  
        $("#pageSize").val(grid.config.pagination.limit);
    });


    //add Plugins
    function selectShowBoxPlugin() {
        $(".gridjs-pagination").css("width","60%");
        return html(
        `<div class="d-flex align-items-center mt-1" id="selectShowBox">
            <span >Shows</span>
            <select class="form-control mx-1 py-1" id="pageSize" style="width:5rem">
                <option value="8" selected>8</option>
                <option value="25">25</option>
                <option value="50">50</option>
            </select>
            <span>entries</span>
        </div>`)
        
    }
    grid.plugin.add({
        id: 'selectShowBoxPlugin',
        component: selectShowBoxPlugin,
        position: PluginPosition.Footer,
    });
    
};
