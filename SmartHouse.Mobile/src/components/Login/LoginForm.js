import React, { useState } from "react";
import {
  TextInput,
  TouchableOpacity,
  StyleSheet,
  KeyboardAvoidingView,
  Button,
  ActivityIndicator
} from "react-native";
import axios from "axios";
import Spinner from 'react-native-loading-spinner-overlay';

const LoginForm = () => {
  const [enteredUsername, setUsername] = useState("");
  const [enteredPassword, setPassword] = useState("");
  const [loading, setLoading] = useState(false);
  const [authorized, setAuthorized] = useState(false);

  const loginHandler = () => {
    setLoading(true);
    axios
      .post("http://e55652a19ec2.ngrok.io/api/users/login", {
        username: enteredUsername,
        password: enteredPassword,
      })
      .then((response) => 
        setAuthorized(true)
        
      )
      .catch((error) => console.log(error));
  };

  const usernameHandler = (val) =>{
    setLoading(false);
    setUsername(val)
  }

  const passwordHandler = (val) =>{
    setLoading(false);
    setPassword(val)
  }

  return (
    <KeyboardAvoidingView behavior="padding" style={styles.container}>
      {loading ? <ActivityIndicator size="large" color="#00ff00"/>: null}
      <TextInput
        onChangeText={(val) => usernameHandler(val)}
        value={enteredUsername}
        placeholder="username"
        placeholderTextColor="rgba(255,255,255,0.7)"
        returnKeyType="next"
        autoCapitalize="none"
        autoCorrect={false}
        style={styles.input}
      />
      <TextInput
        onChangeText={(val) => passwordHandler(val)}
        value={enteredPassword}
        placeholder="password"
        placeholderTextColor="rgba(255,255,255,0.7)"
        returnKeyType="go"
        secureTextEntry
        style={styles.input}
      />

      <TouchableOpacity style={styles.buttonContainer}>
        <Button
          style={styles.buttonText}
          onPress={() => loginHandler()}
          title="Login"
        >
          Login
        </Button>
      </TouchableOpacity>
    </KeyboardAvoidingView>
  );
};

const styles = StyleSheet.create({
  container: {
    padding: 20,
  },
  input: {
    height: 40,
    backgroundColor: "rgba(255,255,255,0.2)",
    marginBottom: 10,
    color: "#FFF",
    paddingHorizontal: 10,
  },
  buttonContainer: {
    backgroundColor: "#2980b9",
    paddingVertical: 15,
  },
  buttonText: {
    textAlign: "center",
    color: "#FFFFFF",
  },
  spinnerTextStyle: {
    color: "#FFF",
  },
});

export default LoginForm;
