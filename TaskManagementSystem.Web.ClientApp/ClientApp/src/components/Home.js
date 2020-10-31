import React, { Component } from 'react';
import TasksTree from './TasksTree';
import TaskInfo from './TaskInfo';

export class Home extends Component {
    static displayName = Home.name;

    constructor(props) {
        super(props);
        this.state = {
            
        };
    }

    setCurrentTask = (task) => {
        this.setState({ CurrentTask: task });
    }

    render () {
        return <div className="row">
          <div className="col-md-5">
                <TasksTree setCurrentTask={this.setCurrentTask}/>
          </div>
          <div className="col-md-7 col-md-offset-5 jumbotron">
                <TaskInfo currentTask={this.state.CurrentTask} />
          </div>
        </div>;
    }
}
