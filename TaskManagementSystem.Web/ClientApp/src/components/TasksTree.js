import React from 'react';
import { Tree } from 'antd';
import axios from 'axios';
const { TreeNode } = Tree;

export default class TasksTree extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            taskList: []
        };
    }

    componentWillMount() {
        this.renderedTasks = {};
        axios.get('/api/gettaskshierarchy')
            .then((responce) => {
                console.log(responce);
                var array = responce.data;
                this.setState({ taskList: array })
            })
            .catch(function (error) {
                alert(error);
            });
    }

    onSelect = (selectedKeys, info) => {
        const url = '/api/gettaskbyid/?id=' + selectedKeys;
        axios.get(url)
            .then((result) => {
                this.props.setCurrentTask(result.data);
                this.forceUpdate();
            })
            .catch((error) => alert(error));
    }

    render() {
        return <div>
            <h3>Задачи</h3>
            <Tree
                showLine
                onSelect={this.onSelect}>
                    {this.state.taskList.length ? this.state.taskList.map(node => this.renderTreeNode(node)) : ''}
            </Tree >
        </div>;
    }

    renderTreeNode(node) {
        return <TreeNode title={node.name} key={node.id} >
            {node.subTasks.length
                ? node.subTasks.map(childNode => this.renderTreeNode(childNode))
                : ''}
        </TreeNode>;
    }
}