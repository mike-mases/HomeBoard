import React, { Component } from 'react';
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
                <h2 class="display-3">Trains</h2>
                <table className='table table-striped' aria-labelledby="tabelLabel">
                    <thead>
                        <tr>
                            <th>Time</th>
                            <th>Destination</th>
                            <th>Platform</th>
                            <th>Expected</th>
                        </tr>
                    </thead>
                    <tbody>
                        {trains.services.map(service =>
                            <tr key={service.time}>
                                <td>{service.time}</td>
                                <td>{service.destination}</td>
                                <td>{service.platform}</td>
                                <td>{service.expected}</td>
                            </tr>
                        )}
                    </tbody>
                </table>
                <p>Last updated: {trains.lastUpdated}</p>
                <h3 class="display-4">Special announcements</h3>
                {trains.specialAnnouncements.map((announcement, index) =>
                    <p key={index} class="lead">{announcement}</p>
                )}
            </div>);
    }
}