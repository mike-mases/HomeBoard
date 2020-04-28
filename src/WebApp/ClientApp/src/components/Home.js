import React, { Component } from 'react';
import { Weather } from './Weather';
import { Trains } from './Trains';
import * as signalR from '@microsoft/signalr';

export class Home extends Component {
  static displayName = Home.name;

  constructor(props) {
    super(props);
    this.state = { weather: {}, trains: {}, loading: true, hubConnection: {} };
  }

  async componentDidMount() {
    await this.getHomeBoardData();
    const hubConnection = new signalR.HubConnectionBuilder()
      .withUrl("/homeBoardUpdateHub")
      .withAutomaticReconnect()
      .configureLogging(signalR.LogLevel.Information)
      .build();

    this.setState({ hubConnection: hubConnection }, () => {
      this.state.hubConnection
        .start()
        .then(() => console.log('Connection started!'))
        .catch(err => console.log('Error while establishing connection :('));
    });

    this.state.hubConnection.on('homeBoardUpdate', (data) => {
      this.setState({ weather: data.weather, trains: data.trains });
    });

    this.state.hubConnection.onclose(() => {
      console.log('Connection disconnected. Waiting 20s');
      setTimeout(function () {
        console.log('Starting connection');
        this.state.hubConnection.start();
      }, 20000);
    });

    this.state.hubConnection.onreconnecting(error => {
      console.log(`Connection lost due to error "${error}". Reconnecting.`);
    });
  }

  render() {
    let weatherContent = this.state.loading
      ? <p><em>Loading...</em></p>
      : <Weather weather={this.state.weather} />;
    let trainsContent = this.state.loading
      ? <p><em>Loading...</em></p>
      : <Trains trains={this.state.trains} />;

    return (
      <div>
        <h1 className="display-1">{this.greetingsMessage()}</h1>
        {weatherContent}
        {trainsContent}
      </div>
    );
  }

  greetingsMessage() {
    var today = new Date()
    var curHr = today.getHours()

    if (curHr < 12) {
      return 'Morning!'
    } else if (curHr < 18) {
      return 'Afternoon!'
    } else {
      return 'Evening!'
    }
  }

  async getHomeBoardData() {
    const response = await fetch('HomeBoard');
    const data = await response.json();
    this.setState({ weather: data.weather, trains: data.trains, loading: false });
  }
}
