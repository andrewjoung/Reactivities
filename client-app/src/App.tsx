import React, {Component} from 'react';
import './App.css';
import axios from 'axios';
import { Header, Icon, List, Container } from 'semantic-ui-react';

class App extends Component {

  state = {
    values: []
  }

  componentDidMount() {

    axios.get("http://localhost:5000/api/values").then((response) => {
      console.log(response);
      this.setState({
        values: response.data
      });
    }).catch((err) => {
      console.log(err);
    })

  }

  render() {
    return (
      <div className="container">

        <Container>
          <Header as='h2'>
            <Icon name='users' />
            <Header.Content>Reactivities</Header.Content>
          </Header>

          <List>
            {this.state.values.map((value: any) => (
              <List.Item key={value.id}>
                {value.name}
              </List.Item>
            ))}
          </List>
        </Container>
      </div>
    );
  }
}

export default App;
