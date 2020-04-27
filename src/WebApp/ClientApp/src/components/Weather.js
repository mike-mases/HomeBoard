import React, { Component } from 'react';
import './Weather.css';

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
                <h2 class="display-3">Weather</h2>
                <div class="container">
                    <div class="row">
                        <div class="col-md-3 col-sm-4 col-lg-3 col-xl-2">
                            <div class={"weather-icon svg-" + weather.icon}></div>
                        </div>
                        <div class="col-md-4 col-sm-4 col-lg-4 col-xl-4 temperature-copy">
                            <p>{weather.temperature.toFixed(1)}&#176;C</p>
                        </div>
                    </div>
                    <div class="row weather-description">
                        <p>The weather for {weather.cityName} is {weather.description}</p>
                    </div>
                    <div class="row last-updated">
                        <p>Last updated: {weather.lastUpdated}</p>
                    </div>
                </div>
            </div>);
    }
}