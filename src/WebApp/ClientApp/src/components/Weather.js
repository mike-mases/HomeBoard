import React, { Component } from 'react';
import { Container, Row, Col } from 'reactstrap';
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
                <h2 className="display-3">Weather</h2>
                <Container>
                    <Row>
                        <Col md="3" sm="4" lg="3" xl="2">
                            <div className={"weather-icon svg-" + weather.icon}></div>
                        </Col>
                        <Col md="4" sm="4" lg="4" xl="4" className="temperature-copy">
                            <p>{weather.temperature.toFixed(1)}&#176;C</p>
                        </Col>
                    </Row>
                    <Row className="weather-description">
                        <p>The weather for {weather.cityName} is {weather.description}</p>
                    </Row>
                    <Row className="last-updated">
                        <p>Last updated: {weather.lastUpdated}</p>
                    </Row>
                </Container>
            </div>);
    }
}