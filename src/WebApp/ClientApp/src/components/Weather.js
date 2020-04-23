import React, { Component } from 'react';

export class Weather extends Component {
    constructor(props) {
        super(props);
        this.state = { weather: props.weather };
    }

    componentWillReceiveProps(props) {
        this.setState({ weather: props.weather })
    }

    render() {
        const weather = this.state.weather;

        return (
            <div>
                <h2>Weather</h2>
                <p>Last updated: {weather.lastUpdated}</p>
                <p>
                    {weather.temperature.toFixed(1)}&#176;C
            </p>
                <p>The weather for {weather.cityName} is {weather.description}</p>
            </div>);
    }
}