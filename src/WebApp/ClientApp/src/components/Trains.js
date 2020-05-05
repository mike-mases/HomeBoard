import React, { Component } from 'react';
// import { Table } from 'reactstrap';
import { CollapsibleTable } from './CollapsibleTable'
import './Trains.css'

export class Trains extends Component {
    constructor(props) {
        super(props);
        this.state = { trains: props.trains, isOpen: false };
    }

    componentWillReceiveProps(props) {
        this.setState({ trains: props.trains })
    }

    render() {
        const trains = this.state.trains;

        // const toggle = () => this.setState({ isOpen: !this.state.isOpen });
        let cols = [
            "Time",
            "Destination",
            "Duration",
            "Platform",
            "Expected",
            "Coaches"
        ];

        return (
            <div>
                <h2 className="display-3">Trains</h2>
                <CollapsibleTable data={trains.services} columns={cols} />
                <div className="last-updated">
                    <p>Last updated: {trains.lastUpdated}</p>
                </div>
                <h3 className="display-4">Special announcements</h3>
                {trains.specialAnnouncements.map((announcement, index) =>
                    <p key={index} className="lead">{announcement}</p>
                )}
            </div>);
    }
}