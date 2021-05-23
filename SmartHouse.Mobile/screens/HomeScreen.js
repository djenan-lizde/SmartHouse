import React, { useState } from "react";
import {
  View,
  Text,
  TextInput,
  StyleSheet,
  SafeAreaView,
  KeyboardAvoidingView,
  TouchableOpacity,
  Button,
} from "react-native";
import axios from "axios";

const HomeScreen = () => {
  const [tempCelsius, setTempCelsius] = useState(0);
  const [tempFahrenheit, setTempFahrenheit] = useState(0);

  const temperatureHandler = (val) => {
    setTempCelsius(val);
    let fahreheit = val * 1.8 + 32;
    setTempFahrenheit(fahreheit);
  };

  const configHandler = () => {
    axios
      .post("https://smarthouseapi20210508183300.azurewebsites.net/api/configuration/save", {
          temperatureCelsius: parseInt(tempCelsius),
          temperatureFahrenheit: tempFahrenheit,
        })
      .then((response) => {
        console.log(response);
      })
      .catch((error) => console.log(error.response));
  };

  return (
    <SafeAreaView style={styles.container}>
      <View style={styles.logoContainer}>
        <Text style={styles.header}>Configuration</Text>
      </View>
      <View style={styles.formContainer}>
        <KeyboardAvoidingView behavior="padding">
          <TextInput
            style={styles.input}
            keyboardType="numeric"
            placeholder="Temperature value"
            returnKeyType="go"
            autoCorrect={false}
            onChangeText={(val) => temperatureHandler(val)}
          />
          <TouchableOpacity style={styles.buttonContainer}>
            <Button
              title="Save"
              style={styles.buttonText}
              onPress={() => configHandler()}
            >
              Save
            </Button>
          </TouchableOpacity>
        </KeyboardAvoidingView>
      </View>
    </SafeAreaView>
  );
};

export default HomeScreen;

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: "#3498db",
  },
  buttonContainer: {
    backgroundColor: "#2980b9",
    borderColor: "#000000",
  },
  buttonText: {
    textAlign: "center",
    color: "#FFFFFF",
  },
  input: {
    height: 40,
    backgroundColor: "rgba(255,255,255,0.2)",
    marginBottom: 20,
    color: "#FFF",
    paddingHorizontal: 15,
    textAlign: "center",
  },
  header: {
    fontSize: 35,
    color: "#FFF",
    marginBottom: 10,
    textAlign: "center",
  },
  formContainer: {
    alignContent: "center",
  },
});
