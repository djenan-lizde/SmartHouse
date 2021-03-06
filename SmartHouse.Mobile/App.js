import React, { useState } from "react";
import axios from "axios";
import Toast from "react-native-toast-message";

import { NavigationContainer } from "@react-navigation/native";
import { createStackNavigator } from "@react-navigation/stack";
import { createDrawerNavigator } from "@react-navigation/drawer";
import AboutUsScreen from "./screens/AboutUsScreen";
import HomeScreen from "./screens/HomeScreen";
import TemperatureScreen from "./screens/TemperatureScreen";
import PhotoScreen from "./screens/PhotoScreen";
import Login from "./src/components/Login/Login";
import Icon from "react-native-vector-icons/Ionicons";

const HomeStack = createStackNavigator();
const AboutUsStack = createStackNavigator();
const TemperatureStack = createStackNavigator();
const PhotoStack = createStackNavigator();
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
        title: "Smart house",
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
    <HomeStack.Screen name="Temperature" component={TemperatureScreen} />
  </HomeStack.Navigator>
);

const AboutUsStackScreen = ({ navigation }) => (
  <AboutUsStack.Navigator
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
    <AboutUsStack.Screen
      name="About us"
      component={AboutUsScreen}
      options={{
        title: "Smart house",
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
  </AboutUsStack.Navigator>
);

const PhotoStackScreen = ({ navigation }) => (
  <PhotoStack.Navigator
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
    <PhotoStack.Screen
      name="Photos"
      component={PhotoScreen}
      options={{
        title: "Smart house",
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
  </PhotoStack.Navigator>
);

const TemperatureStackScreen = ({ navigation }) => (
  <TemperatureStack.Navigator
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
    <TemperatureStack.Screen
      name="Temperature"
      component={TemperatureScreen}
      options={{
        title: "Smart house",
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
  </TemperatureStack.Navigator>
);

const App = () => {
  const [authorized, setAuthorized] = useState(false);
  const [enteredUsername, setUsername] = useState("");
  const [enteredPassword, setPassword] = useState("");
  const [loading, setLoading] = useState(false);

  const loginHandler = () => {
    setLoading(true);
    axios
      .post(
        "https://smarthouseapi20210508183300.azurewebsites.net/api/users/login",
        {
          username: enteredUsername,
          password: enteredPassword,
        }
      )
      .then((response) => {
        if (response.status == 200) {
          setAuthorized(true);
        }
        setLoading(false);
      })
      .catch((error) => {
        Toast.show({
          type:"error",
          text1: "Error!",
          text2: "Wrong username or password. Try again!",
          position: "top",
          visibilityTime: 5000,
          autoHide: true,
        });
        setLoading(false);
      });
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
            <Drawer.Screen
              name="Temperature"
              component={TemperatureStackScreen}
            />
            <Drawer.Screen name="Photos" component={PhotoStackScreen} />
            <Drawer.Screen name="About us" component={AboutUsStackScreen} />
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
      <Toast ref={(ref) => Toast.setRef(ref)} />
    </>
  );
};

export default App;
