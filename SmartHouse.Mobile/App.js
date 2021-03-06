import React, { useState } from "react";
import axios from "axios";

import { ActivityIndicator } from "react-native";
import { NavigationContainer } from "@react-navigation/native";
import { createStackNavigator } from "@react-navigation/stack";
import { createDrawerNavigator } from "@react-navigation/drawer";
import DetailsScreen from "./screens/DetailsScreen";
import HomeScreen from "./screens/HomeScreen";
import Login from "./src/components/Login/Login";
import Icon from "react-native-vector-icons/Ionicons";

const HomeStack = createStackNavigator();
const DetailsStack = createStackNavigator();
const Drawer = createDrawerNavigator();

const HomeStackScreen = ({ navigation }) => (
  <HomeStack.Navigator
    screenOptions={{
      headerStyle: {
        backgroundColor: "#3498db",
      },
      headerTintColor: "#fff",
      headerTitleStyle: {
        fontWeight: "bold",
      },
    }}
  >
    <HomeStack.Screen
      name="Home"
      component={HomeScreen}
      options={{
        title: "Overview",
        headerLeft: () => (
          <Icon.Button
            name="ios-menu"
            size={25}
            backgroundColor="#3498db"
            onPress={() => navigation.openDrawer()}
          ></Icon.Button>
        ),
      }}
    />
    <HomeStack.Screen name="Details" component={DetailsScreen} />
  </HomeStack.Navigator>
);

const DetailsStackScreen = ({ navigation }) => (
  <DetailsStack.Navigator
    screenOptions={{
      headerStyle: {
        backgroundColor: "#3498db",
      },
      headerTintColor: "#fff",
      headerTitleStyle: {
        fontWeight: "bold",
      },
    }}
  >
    <DetailsStack.Screen
      name="Details"
      component={DetailsScreen}
      options={{
        title: "Overview",
        headerLeft: () => (
          <Icon.Button
            name="ios-menu"
            size={25}
            backgroundColor="#3498db"
            onPress={() => navigation.openDrawer()}
          ></Icon.Button>
        ),
      }}
    />
  </DetailsStack.Navigator>
);

const App = () => {
  const [authorized, setAuthorized] = useState(false);
  const [enteredUsername, setUsername] = useState("");
  const [enteredPassword, setPassword] = useState("");
  const [loading, setLoading] = useState(false);

  const loginHandler = () => {
    setLoading(true);
    axios
      .post("http://257912c31eed.ngrok.io/api/users/login", {
        username: enteredUsername,
        password: enteredPassword,
      })
      .then((response) => {
        console.log(response.data.token);
        setAuthorized(true);
        setLoading(false);
        navigation.navigate("HomeScreen");
      })
      .catch((error) => console.log(error));
  };

  const usernameHandler = (val) => {
    setLoading(false);
    setUsername(val);
  };

  const passwordHandler = (val) => {
    setLoading(false);
    setPassword(val);
  };

  return (
    <>
      {authorized ? (
        <NavigationContainer>
          <Drawer.Navigator initialRouteName="Home">
            <Drawer.Screen name="Home" component={HomeStackScreen} />
            <Drawer.Screen name="Details" component={DetailsStackScreen} />
          </Drawer.Navigator>
        </NavigationContainer>
      ) : (
        <Login
          loading={loading}
          usernameChange={(val) => usernameHandler(val)}
          passwordChange={(val) => passwordHandler(val)}
          login={() => loginHandler()}
        />
      )}
    </>
  );
};

export default App;
