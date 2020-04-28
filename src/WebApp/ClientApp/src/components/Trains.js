import React, { Component } from 'react';
import { Table } from 'reactstrap';
import './Trains.css'

export class Trains extends Component {
    constructor(props) {
        super(props);
        this.state = { trains: props.trains };
    }

    componentWillReceiveProps(props) {
        this.setState({ trains: props.trains })
    }

    render() {
        const trains = this.state.trains;

        return (
            <div>
                <h2 className="display-3">Trains</h2>

                <Table striped>
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
                                <tr key={service.time}>
                                    <td>{service.time}</td>
                                    <td>{service.destination.name}</td>
                                    <td>{service.destination.duration}m</td>
                                    <td>{service.platform}</td>
                                    <td data-ontime={service.expected == "On time"}>{service.expected}</td>
                                    <td>{service.coaches}</td>
                                </tr>
                                // <tr key={service.time + "-callingat"}>
                                //     <td colSpan="5"><div className="marquee"><p>{service.callingAt.join(' ')}</p></div></td>
                                // </tr>
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