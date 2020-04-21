import React, { Component } from 'react';
import { Weather } from './Weather';
import { Trains } from './Trains';

export class Home extends Component {
  static displayName = Home.name;

  constructor(props) {
    super(props);
    this.state = { weather: {}, trains: {}, loading: true };
  }

  componentDidMount() {
    this.getHomeBoardData();
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
        <h1>{this.greetingsMessage()}</h1>
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
