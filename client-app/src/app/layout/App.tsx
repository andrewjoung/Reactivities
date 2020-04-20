import React, { useState, useEffect, Fragment } from 'react';
import axios from 'axios';
import { Header, Icon, List, Container } from 'semantic-ui-react';
import {IActivity} from "../models/activity";
import Navbar from "../../Features/nav/Navbar";

const App = () => {

  // [the state, set state]
  const [activities, setActivities] = useState<IActivity[]>([]);

  // useEffect hook => has 3 lifecycle methods
  // Essentially works as component did mount
  // Empty [] ensures that the useEffect runs once
  useEffect(() => {
    axios.get<IActivity[]>("http://localhost:5000/api/activities").then((response) => {
      console.log(response);
      setActivities(response.data);
    }).catch((err) => {
      console.log(err);
    });  
  }, []);

  return (
    <div>
      <Navbar />

      <Container style={{marginTop: '7em'}}>
        <List>
          {activities.map((activity) => (
            <List.Item key={activity.id}>
              {activity.title}
            </List.Item>
          ))}
        </List>
      </Container>
    </div>
  );
}

export default App;
