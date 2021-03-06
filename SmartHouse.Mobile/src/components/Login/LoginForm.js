import React  from "react";
import {
  TextInput,
  TouchableOpacity,
  StyleSheet,
  KeyboardAvoidingView,
  Button
} from "react-native";

const LoginForm = (props) => {
  return (
    <KeyboardAvoidingView behavior="padding" style={styles.container}>
      <TextInput
        onChangeText={(val) => props.usernameChange(val)}
        placeholder="username"
        placeholderTextColor="rgba(255,255,255,0.7)"
        returnKeyType="next"
        autoCapitalize="none"
        autoCorrect={false}
        style={styles.input}
      />
      <TextInput
        onChangeText={(val) =>props.passwordChange(val)}
        placeholder="password"
        placeholderTextColor="rgba(255,255,255,0.7)"
        returnKeyType="go"
        autoCapitalize="none"
        secureTextEntry
        style={styles.input}
      />

      <TouchableOpacity style={styles.buttonContainer}>
        <Button
          style={styles.buttonText}
          onPress={props.login}
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
