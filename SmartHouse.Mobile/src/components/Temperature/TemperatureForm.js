import React, { useState } from "react";
import {
  StyleSheet,
  TouchableOpacity,
  Button,
  KeyboardAvoidingView,
  View,
  Text,
} from "react-native";
import axios from "axios";
import DateTimePicker from "@react-native-community/datetimepicker";

const TemperatureForm = () => {
  const [date, setDate] = useState(new Date());
  const [show, setShow] = useState(false);
  const [showTemp, setShowTemp] = useState(false);
  const [avgCelTemp, setAvgCelTemp] = useState();
  const [avgFahTemp, setAvgFahTemp] = useState();

  const onChange = (event, selectedDate) => {
    setDate(selectedDate);
    setShow(false);
    console.log(date);
    axios
      .get(
        "http://b00667dd621f.ngrok.io/api/temperatures/filter/" +
          date.toDateString()
      )
      .then((response) => {
        console.log(response.data);
        setAvgCelTemp(response.data.celAvgTemperature);
        setAvgFahTemp(response.data.fahAvgTemperature);
        setShowTemp(true);
      })
      .catch((error) => console.log(error));
  };

  const showDatepicker = () => {
    setShow(true);
  };

  return (
    <KeyboardAvoidingView behavior="padding" style={styles.container}>
      {show ? (
        <DateTimePicker
          testID="dateTimePicker"
          value={date}
          is24Hour={false}
          display="default"
          onChange={onChange}
        />
      ) : null}
      {showTemp ? (
        <View style={styles.tempContainer}>
          <Text>Average temperature celsius: {avgCelTemp}°C</Text>
          <Text>Average temperature fahrenheit: {avgFahTemp}°F</Text>
        </View>
      ) : null}
      <TouchableOpacity style={styles.buttonContainer}>
        <Button
          style={styles.buttonText}
          onPress={showDatepicker}
          title="Show date picker!"
        >
          Show picker
        </Button>
      </TouchableOpacity>
    </KeyboardAvoidingView>
  );
};

export default TemperatureForm;

const styles = StyleSheet.create({
  container: {
    padding: 20,
  },
  buttonContainer: {
    backgroundColor: "#2980b9",
    borderColor: "#000000",
  },
  buttonText: {
    textAlign: "center",
    color: "#FFFFFF",
  },
  tempContainer: {
    marginBottom:400,
    fontSize:20
  },
});
