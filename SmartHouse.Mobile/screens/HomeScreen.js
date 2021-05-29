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
import Toast from "react-native-toast-message";

const HomeScreen = () => {
  const [tempCelsius, setTempCelsius] = useState(0);
  const [tempFahrenheit, setTempFahrenheit] = useState(0);

  const temperatureHandler = (val) => {
    if(isNaN(val)){
      Toast.show({
        type: "error",
        text1: "Invalid value!",
        text2: "Temperature must be a numeric value!",
        position: "top",
        visibilityTime: 5000,
        autoHide: true,
      });
    }

    const temp = parseFloat(val);

    if(temp < 1  || temp > 55) {
      Toast.show({
        type: "error",
        text1: "Invalid value!",
        text2: "Temperature must be between 1 and 55 celsius degrees!",
        position: "top",
        visibilityTime: 5000,
        autoHide: true,
      });
    }

    setTempCelsius(temp);
    let fahreheit = parseFloat(temp * 1.8 + 32);
    setTempFahrenheit(fahreheit);
  };

  const configHandler = () => {
    axios
      .post(
        "https://smarthouseapi20210508183300.azurewebsites.net/api/configuration/save",
        {
          temperatureCelsius: tempCelsius,
          temperatureFahrenheit: tempFahrenheit,
        }
      )
      .then((response) => {
        Toast.show({
          type: "success",
          text1: "Configuration saved!",
          text2: "System will respond at the configured temperature!",
          position: "top",
          visibilityTime: 5000,
          autoHide: true,
        });
      })
      .catch((error) => {
        Toast.show({
          type: "error",
          text1: "Error!",
          text2: "Something went wrong!",
          position: "top",
          visibilityTime: 5000,
          autoHide: true,
        });
      });
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
            placeholder="Temperature treshold in Celsius"
            placeholderTextColor="rgba(255,255,255,0.7)"
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
    marginTop: 20
  },
  formContainer: {
    alignContent: "center",
    padding: 40,
    width:"90%",
    alignSelf:"center"
  },
});
