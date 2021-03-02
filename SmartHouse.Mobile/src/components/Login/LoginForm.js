import React, { useState } from "react";
import {
  TextInput,
  TouchableOpacity,
  StyleSheet,
  KeyboardAvoidingView,
  Button,
  Alert
} from "react-native";

const LoginForm = () => {
  const [enteredUsername, setUsername] = useState("");
  const [enteredPassword, setPassword] = useState("");

  const loginHandler = () => {
    Alert.alert("IDe gas")
  };

  return (
    <KeyboardAvoidingView behavior="padding" style={styles.container}>
      <TextInput
        onChangeText={(val) => setUsername(val)}
        value={enteredUsername}
        placeholder="username"
        placeholderTextColor="rgba(255,255,255,0.7)"
        returnKeyType="next"
        autoCapitalize="none"
        autoCorrect={false}
        style={styles.input}
      />
      <TextInput
        onChangeText={(val) => setPassword(val)}
        value={enteredPassword}
        placeholder="password"
        placeholderTextColor="rgba(255,255,255,0.7)"
        returnKeyType="go"
        secureTextEntry
        style={styles.input}
      />

      <TouchableOpacity style={styles.buttonContainer}>
        <Button style={styles.buttonText} onPress={() => loginHandler()} title="Login">Login</Button>
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
});

export default LoginForm;