import React from 'react';
import axios from 'axios';
import NewTaskModal from './NewTaskModal';
import './TaskInfo.css';

export default class TaskInfo extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
        };
    }

    componentWillReceiveProps(nextProps) {
        if (nextProps.currentTask)
            this.setState({
                Id: nextProps.currentTask.id,
                Name: nextProps.currentTask.name,
                Description: nextProps.currentTask.description,
                DateOfIssue: nextProps.currentTask.dateOfIssue,
                DateOfCompletion: nextProps.currentTask.dateOfCompletion,
                Executors: nextProps.currentTask.executors,
                State: nextProps.currentTask.state,
                ActualCompletionHours: nextProps.currentTask.actualCompletionHours,
                PlannedCompletionHours: nextProps.currentTask.plannedCompletionHours,
                SubTasks: nextProps.currentTask.subTasks
            });
    }

    handleNameChange = event => {
        this.setState({ Name: event.target.value });
    }

    handleDescriptionChange = event => {
        this.setState({ Description: event.target.value });
    }

    handleExecutorsChange = event => {
        this.setState({ Executors: event.target.value });
    }

    handleTaskStateChange = event => {
        this.setState({ State: this.taskStates.indexOf(event.target.value) });
    }

    handlePlannedHoursChange = event => {
        this.setState({ PlannedCompletionHours: event.target.value });
    }

    handleActualHoursChange = event => {
        this.setState({ ActualCompletionHours: event.target.value });
    }

    handleSubmit = event => {
        event.preventDefault();

        const task = {
            Name: this.state.Name,
            Description: this.state.Description,
            Id: this.state.Id,
            State: this.state.State,
            Executors: this.state.Executors,
            DateOfIssue: this.state.DateOfIssue,
            DateOfCompletion: this.state.DateOfCompletion,
            ActualCompletionHours: this.state.ActualCompletionHours,
            PlannedCompletionHours: this.state.PlannedCompletionHours,
            SubTasks: this.state.SubTasks
        };

        axios.post(`${process.env.REACT_APP_BACKEND_URL}/api/UpdateTask`, task)
            .catch((error) => alert(error));

    }

    deleteTask = event => {
        event.preventDefault();

        const id = {
            value: this.state.Id,
        }

        axios.post(`${process.env.REACT_APP_BACKEND_URL}/api/DeleteTask`, id)
            .then(() => {
                alert('Задача удалена');
                window.location.reload();
            })
            .catch((error) => alert(error));
    }

    getSubTasksHours(subTasks, actual) {
        let sum = 0;
        if (subTasks)
            for (var i = 0; i < subTasks.length; i++) {
                sum += actual ? subTasks[i].actualCompletionHours : subTasks[i].plannedCompletionHours;
                sum += this.getSubTasksHours(subTasks[i].subTasks, actual);
            }
        return sum;
    }

    taskStates = [
        'Назначена',
        'Выполняется',
        'Приостановлена',
        'Завершена'
    ];

    render() {
        return <div>
            <h3>Информация о задаче {this.state ? this.state.Name : ''}</h3>
            <label>{this.state.DateOfIssue ? 'Создана: ' + this.state.DateOfIssue.substring(0, this.state.DateOfIssue.indexOf('T'))
                    + ' ' + this.state.DateOfIssue.substring(this.state.DateOfIssue.indexOf('T') + 1) + '; ' : ''}
                {this.state.DateOfCompletion ? 'Завершена: ' + this.state.DateOfCompletion.substring(0, this.state.DateOfCompletion.indexOf('T')) + 
                    ' ' + this.state.DateOfCompletion.substring(this.state.DateOfCompletion.indexOf('T') + 1) : ''}</label>
            <form method="post" onSubmit={this.handleSubmit}>
                <div className="form-group">
                    <label>Название задачи</label>
                    <input type="text" className="form-control"
                        value={this.state.Name ? this.state.Name : ''}
                        onChange={this.handleNameChange} />
                </div>
                <div className="form-group">
                    <label>Описание задачи</label>
                    <input type="text" className="form-control"
                        value={this.state.Description ? this.state.Description : ''}
                        onChange={this.handleDescriptionChange} />
                </div>
                <div className="form-group">
                    <label>Исполнители</label>
                    <input type="text" className="form-control"
                        value={this.state.Executors ? this.state.Executors : ''}
                        onChange={this.handleExecutorsChange} />
                </div>
                <div className="form-group" >
                    <label>Плановая трудоемкость задачи (Плановая трудоемкость подзадач: {this.getSubTasksHours(this.state.SubTasks, false)} часов)</label>
                    <input type='number' className='form-control' value={this.state.PlannedCompletionHours} onChange={this.handlePlannedHoursChange} min='0' step='1' />
                </div>
                <div className="form-group">
                    <label>Фактическое время выполнения (Время выполнения подзадач: {this.getSubTasksHours(this.state.SubTasks, true)} часов)</label>
                    <input type='number' className='form-control' value={this.state.ActualCompletionHours} onChange={this.handleActualHoursChange} min='0' step='1' />
                </div>
                <div className='form-group'>
                    <label>Состояние задачи</label>
                    <select className='form-control' value={this.taskStates[this.state.State]} onChange={this.handleTaskStateChange}>
                        {this.taskStates.map(state => <option>{state}</option>)}}
                    </select>
                </div>
                <button type="submit" className="btn btn-outline-success">Сохранить</button>
            </form>
            <hr />
            <div>
                <NewTaskModal parentTask={this.state} />
                <button className="btn btn-outline-danger" onClick={this.deleteTask} >Удалить задачу</button>
            </div>
        </div>;
    }

}