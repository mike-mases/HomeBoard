import React, { Component } from 'react';
import { Table } from 'reactstrap';
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

        const toggle = () => this.setState({ isOpen: !this.state.isOpen });

        return (
            <div>
                <h2 className="display-3">Trains</h2>

                <Table striped className="fold-table">
                    <colgroup>
                        <col style={{ width: 15 + '%' }} />
                        <col style={{ width: 25 + '%' }} />
                        <col style={{ width: 15 + '%' }} />
                        <col style={{ width: 15 + '%' }} />
                        <col style={{ width: 20 + '%' }} />
                        <col style={{ width: 10 + '%' }} />
                    </colgroup>
                    <thead>
                        <tr>
                            <th>Time</th>
                            <th>Destination</th>
                            <th>Duration</th>
                            <th>Platform</th>
                            <th>Expected</th>
                            <th>Coaches</th>
                        </tr>
                    </thead>
                    <tbody>
                        {trains.services.map(service => {
                            return [
                                <tr key={service.time} onClick={toggle}>
                                    <td>{service.time}</td>
                                    <td>{service.destination.name}</td>
                                    <td>{service.destination.duration}m</td>
                                    <td>{service.platform}</td>
                                    <td data-ontime={service.expected == "On time"}>{service.expected}</td>
                                    <td>{service.coaches}</td>
                                </tr>,
                                <tr className={this.state.isOpen ? "open" : "closed"}>
                                    <td colSpan="3">{service.callingAt.map((callingPoint, index) => {
                                        return <p key={index}>{callingPoint}</p>
                                    })}
                                    </td>
                                    <td colSpan="3">{service.lastReport}</td>
                                </tr>

                            ];
                        }
                        )}
                    </tbody>
                </Table>
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