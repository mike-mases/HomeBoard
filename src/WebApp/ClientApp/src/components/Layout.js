import React, { Component } from 'react';
import { Container } from 'reactstrap';
import './Layout.css';

export class Layout extends Component {
  static displayName = Layout.name;

  render () {
    return (
      <div className="main-layout">
        <Container>
          {this.props.children}
        </Container>
      </div>
    );
  }
}
