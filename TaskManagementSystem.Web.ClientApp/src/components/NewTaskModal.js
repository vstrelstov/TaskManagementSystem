import React from 'react';
import { Button, Header, Icon, Modal } from 'semantic-ui-react';
import axios from 'axios';
import 'semantic-ui-css/semantic.min.css';

export default class NewTaskModal extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            modalOpen: false,
            isRootTask: false,
            parentTask: this.props.parentTask,
        };
    }

    isRootTaskChanged = event => {
        this.setState({ isRootTask: !this.state.isRootTask });
    }

    handleOpen = () => this.setState({ modalOpen: true })

    handleClose = () => this.setState({ modalOpen: false })

    handleNameChange = event => {
        this.setState({ Name: event.target.value });
    }

    handleDescriptionChange = event => {
        this.setState({ Description: event.target.value });
    }

    handleExecutorsChange = event => {
        this.setState({ Executors: event.target.value });
    }

    handlePlannedHoursChange = event => {
        this.setState({ PlannedCompletionHours: event.target.value });
    }

    handleActualHoursChange = event => {
        this.setState({ ActualCompletionHours: event.target.value });
    }

    handleSubmit = event => {
        event.preventDefault();

        const newTask = {
            Name: this.state.Name,
            Description: this.state.Description,
            Executors: this.state.Executors,
            PlannedCompletionHours: this.state.PlannedCompletionHours,
            ActualCompletionHours: this.state.ActualCompletionHours,
            ParentTask: this.state.isRootTask || this.state.parentTask.Id === undefined ? null : this.state.parentTask
        };
		
		console.log(process.env.REACT_APP_BACKEND_URL);

        axios.post(`${process.env.REACT_APP_BACKEND_URL}/api/AddTask`, newTask)
            .then(() => {
                    alert('Новая задача успешно добавлена!');
                    this.handleClose();
                    window.location.reload();
                })
            .catch((error) => alert(error));
    }

    render() {
        return <Modal trigger={<Button className="btn btn-outline-success" onClick={this.handleOpen}>Добавить задачу</Button>}
                open={this.state.modalOpen}
                onClose={this.handleClose}
                basic size='small'
                centered={false}>
            <Header content='Добавить новую задачу' icon='tasks' />
            <Modal.Content>
                {this.renderContent()}
            </Modal.Content>
            <Modal.Actions>
                <Button color='green' onClick={this.handleSubmit} inverted>
                    <Icon name='plus' /> Создать задачу
                </Button>
                <Button basic color='red' onClick= { this.handleClose } inverted>
                    <Icon name='remove' /> Закрыть
                </Button>
            </Modal.Actions>
        </Modal>;
    }

    renderContent() {
        return <div>
            <div className='form-group'>
                <input type='checkbox' value={this.state.isRootTask} onChange={this.isRootTaskChanged} /> Корневая задача
            </div>
            <form>
                <div className='form-group'>
                    <label>Название задачи</label>
                    <input type='text' className='form-control' value={this.state.Name} onChange={this.handleNameChange} />
                </div>
                <div className='form-group'>
                    <label>Описание задачи</label>
                    <input type='text' className='form-control' value={this.state.Description} onChange={this.handleDescriptionChange}/>
                </div>
                <div className='form-group'>
                    <label>Исполнители</label>
                    <input type='text' className='form-control' value={this.state.Executors} onChange={this.handleExecutorsChange} />
                </div>
                <div className='form-group'>
                    <label>Плановая трудоемкость</label>
                    <input type='number' className='form-control' value={this.state.PlannedCompletionHours} onChange={this.handlePlannedHoursChange} min='0' step='1' />
                </div>
                <div className='form-group'>
                    <label>Фактическое время выполнения</label>
                    <input type='number' className='form-control' value={this.state.ActualCompletionHours} onChange={this.handleActualHoursChange} min='0' step='1' />
                </div>
            </form>
        </div>;
    }
}