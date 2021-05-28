import React from "react";
import {
  View,
  StyleSheet,
  Image,
  SafeAreaView,
  ActivityIndicator,
  Text,
} from "react-native";
import LoginForm from "./LoginForm";

const Login = (props) => {
  return (
    <SafeAreaView style={styles.container}>
      <View style={styles.logoContainer}>
        <Text style={styles.header}>Smart house</Text>
        <Image
          style={styles.logo}
          source={require("../../../assets/house.jpg")}
        />
      </View>
      {props.loading ? <ActivityIndicator size="large" color="white" /> : null}
      <View style={styles.formContainer}>
        <LoginForm
          usernameChange={props.usernameChange}
          passwordChange={props.passwordChange}
          login={props.login}
        />
      </View>
    </SafeAreaView>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: "#3498db",
  },
  logoContainer: {
    alignItems: "center",
    flexGrow: 1,
    justifyContent: "center",
  },
  logo: {
    width: 100,
    height: 100,
  },
  formContainer: {
    marginBottom: 30,
  },
  header: {
    fontSize: 35,
    color: "#FFF",
    marginBottom: 10,
  },
});

export default Login;
