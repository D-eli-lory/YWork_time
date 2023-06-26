const YAnswer={template:`
<div>

<button type="button"
class="btn btn-primary m-2 fload-end"
data-bs-toggle="modal"
data-bs-target="#exampleModal"
@click="addClick()">
Добавить ответ
</button>

<table class="table table-striped">
<thead>
    <tr>
        <th>
            ID
        </th>
        <th>
            Задача
        </th>
        <th>
        <div class="d-flex flex-row">

            <input class="form-control m-2" 
                v-model="Date_workFilter"
                v-on:keyup="FilterFn()"
                placeholder="Поиск">

            </div>
            Дата
        </th>
        <th>
            Время
        </th>
        <th>
            Комментарии
        </th>
        <th>
            Опции
        </th>

    </tr>
</thead>
<tbody>
    <tr v-for="ya in YAnswers">
        <td>{{ya.AnswerID}}</td>
        <td>{{ya.TaskName}}</td>
        <td>{{ya.Date_work}}</td>
        <td>{{ya.Kol_time}}</td>
        <td>{{ya.Comments}}</td>
        <td>
            <button type="button"
            class="btn btn-light mr-1"
            data-bs-toggle="modal"
            data-bs-target="#exampleModal"
            @click="editClick(ya)">
                <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-pencil-square" viewBox="0 0 16 16">
                <path d="M15.502 1.94a.5.5 0 0 1 0 .706L14.459 3.69l-2-2L13.502.646a.5.5 0 0 1 .707 0l1.293 1.293zm-1.75 2.456-2-2L4.939 9.21a.5.5 0 0 0-.121.196l-.805 2.414a.25.25 0 0 0 .316.316l2.414-.805a.5.5 0 0 0 .196-.12l6.813-6.814z"/>
                <path fill-rule="evenodd" d="M1 13.5A1.5 1.5 0 0 0 2.5 15h11a1.5 1.5 0 0 0 1.5-1.5v-6a.5.5 0 0 0-1 0v6a.5.5 0 0 1-.5.5h-11a.5.5 0 0 1-.5-.5v-11a.5.5 0 0 1 .5-.5H9a.5.5 0 0 0 0-1H2.5A1.5 1.5 0 0 0 1 2.5v11z"/>
                </svg>
            </button>
            <button type="button" @click="deleteClick(ya.AnswerID)"
            class="btn btn-light mr-1">
                <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-trash3-fill" viewBox="0 0 16 16">
                <path d="M11 1.5v1h3.5a.5.5 0 0 1 0 1h-.538l-.853 10.66A2 2 0 0 1 11.115 16h-6.23a2 2 0 0 1-1.994-1.84L2.038 3.5H1.5a.5.5 0 0 1 0-1H5v-1A1.5 1.5 0 0 1 6.5 0h3A1.5 1.5 0 0 1 11 1.5Zm-5 0v1h4v-1a.5.5 0 0 0-.5-.5h-3a.5.5 0 0 0-.5.5ZM4.5 5.029l.5 8.5a.5.5 0 1 0 .998-.06l-.5-8.5a.5.5 0 1 0-.998.06Zm6.53-.528a.5.5 0 0 0-.528.47l-.5 8.5a.5.5 0 0 0 .998.058l.5-8.5a.5.5 0 0 0-.47-.528ZM8 4.5a.5.5 0 0 0-.5.5v8.5a.5.5 0 0 0 1 0V5a.5.5 0 0 0-.5-.5Z"/>
                </svg>
            </button>

        </td>
    </tr>
</tbody>
</thead>
</table>

<div class="modal fade" id="exampleModal" tabindex="-1"
    aria-labelledby="exampleModalLabel" aria-hidden="true">
<div class="modal-dialog modal-lg modal-dialog-centered">
<div class="modal-content">
    <div class="modal-header">
        <h5 class="modal-title" id="exampleModalLabel">{{modalTitle}}</h5>
        <button type="button" class="btn-close" data-bs-dismiss="modal"
        aria-label="Close"></button>
    </div>

    <div class="modal-body">

        <div class="input-group mb-3">
        <span class="input-group-text">Задача</span>
        <select class="form-select" v-model="TaskName">
            <option v-for="yt in Tasks">
            {{yt.TaskName}}
            </option>
        </select>
        </div>

        <div class="input-group mb-3">
        <span class="input-group-text">Дата</span>
        <input type="date" class="form-control" v-model="Date_work">
        </div>

        <div class="input-group mb-3">
            <span class="input-group-text">Количество</span>
            <input type="number" min="1" max="24" class="form-control" v-model="Kol_time">
        </div>

        <div class="input-group mb-3">
            <span class="input-group-text">Комментарии</span>
            <input type="text" class="form-control" v-model="Comments">
        </div>

        <button type="button" @click="createClick()"
        v-if="AnswerID==0" class="btn btn-primary">
        Создать
        </button>
        <button type="button"  @click="updateClick()"
        v-if="AnswerID!=0" class="btn btn-primary">
        Изменить
        </button>

    </div>

</div>
</div>
</div>
</div>

`,
data(){
    return{
        YAnswers:[],
        modalTitle:"",
        AnswerID:0,
        TaskName:"",
        Date_work:"",
        Kol_time:0,
        Comments:"",
        Date_workFilter:"",
        DatesWithoutFilter:[]
    }
},
methods:{
    refreshData(){
        axios.get(variables.API_URL+"YAnswer")
        .then((response)=>{
            this.YAnswers=response.data;
            this.DatesWithoutFilter=response.data;
        });
        axios.get(variables.API_URL+"YTask")
        .then((response)=>{
            this.Tasks=response.data;
        });
    },
    addClick(ya){
        this.modalTitle="Добавить задачу"
        this.AnswerID=0;
        this.TaskName="";
        this.Date_work="";
        this.Kol_time= 0;
        this.Comments="";
    },
    editClick(ya){
        this.modalTitle="Изменить задачу"
        this.AnswerID=ya.AnswerID;
        this.TaskName=ya.TaskName;
        this.Date_work=ya.Date_work;
        this.Kol_time=ya.Kol_time;
        this.Comments=ya.Comments;
    },
    createClick(){
        axios.post(variables.API_URL+"YAnswer",{
            TaskName:this.TaskName,
            Date_work:this.Date_work,
            Kol_time:this.Kol_time,
            Comments:this.Comments
        })
        .then((response)=>{
            this.refreshData();
            alert(response.data);
        });
    },
    updateClick(){
        axios.put(variables.API_URL+"YAnswer",{
            AnswerID:this.AnswerID,
            TaskName:this.TaskName,
            Date_work:this.Date_work,
            Kol_time:this.Kol_time,
            Comments:this.Comments
        })
        .then((response)=>{
            this.refreshData();
            alert(response.data);
        });
    },
    deleteClick(id){
        if(!confirm("Вы уверены?")){
            return;
        }
        axios.delete(variables.API_URL+"YAnswer/"+id)
        .then((response)=>{
            this.refreshData();
            alert(response.data);
        });

    },
    FilterFn(){
        var Date_workFilter=this.Date_workFilter;

        this.YAnswers=this.DatesWithoutFilter.filter(
            function(el){
                return el.Date_work.toString().toLowerCase().includes(
                    Date_workFilter.toString().trim().toLowerCase()
                )
            });
    }
},
mounted:function(){
    this.refreshData();
}
}
