import './CollapsibleTable.css'
import React, { Component } from 'react';

class RowItem extends Component {

    constructor() {
        super();

        this.state = {
            open: false
        }
    }

    toggleRow(e) {
        this.setState({ open: !this.state.open });
    }

    render() {

        let classes = 'outer';
        if (this.state.open) {
            classes = 'outer open';
        }

        return (
            <li onClick={this.toggleRow.bind(this)} className={classes}>
                <div className="heading">
                    <div className="col">{this.props.time}</div>
                    <div className="col">{this.props.destination.name}</div>
                    <div className="col">{this.props.destination.duration}m</div>
                    <div className="col">{this.props.platform}</div>
                    <div className="col" data-ontime={this.props.expected == "On time"}>{this.props.expected}</div>
                    <div className="col">{this.props.coaches}</div>
                </div>
                <RowContent open={this.state.open} callingAt={this.props.callingAt} lastReport={this.props.lastReport} />
                {this.props.children}
            </li>
        )
    }
};

class RowContent extends Component {
    constructor(props) {
        super(props);

        this.state = {
            openHeight: props.callingAt.length * 35 + 90 / props.callingAt.length
        }
    }

    clicker() {
    }

    render() {
        const callingPoints = (<ul>
            {this.props.callingAt.map((callingPoint, index) => {
                return (<li key={index}>{callingPoint}</li>)
            })}
        </ul>);

        const lastReport = (<div className='last-report'>Last report: {this.props.lastReport}</div>);

        let jsxhtml = (<div className="content" onClick={this.clicker.bind(this)}>
            {callingPoints}
            {lastReport}
            {this.props.children}
        </div>);

        if (this.props.open) {
            jsxhtml = (<div className="content open" onClick={this.clicker.bind(this)} style={{ height: this.state.openHeight + 'px' }}>
                {callingPoints}
                {lastReport}
                {this.props.children}
            </div>);
        }

        return (
            <div>
                {jsxhtml}
            </div>
        )
    }
};


export class CollapsibleTable extends Component {
    render() {

        let columns = this.props.columns.map((item, index) => {
            return (<HeaderColumn key={index} label={item} />);
        });

        //go through the rows
        let rows = this.props.data.map((item, index) => {
            return (<RowItem key={index} {...item}></RowItem>);
        });

        return (<div className="table">
            {this.props.children}
            <div className='header' ref="header">{columns}</div>
            <ul>{rows}</ul>
        </div>);
    }
}

class HeaderColumn extends Component {
    render() {
        return (<div className="hcol">{this.props.label}</div>);
    }
}