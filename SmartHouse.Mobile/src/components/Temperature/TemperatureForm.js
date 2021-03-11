import React, { useState } from "react";
import {
  StyleSheet,
  TouchableOpacity,
  Button,
  KeyboardAvoidingView,
} from "react-native";
import axios from "axios";
import DateTimePicker from "@react-native-community/datetimepicker";
const queryString = require('query-string');

const TemperatureForm = () => {
  const [date, setDate] = useState(new Date());
  const [show, setShow] = useState(false);

  const onChange = (event, selectedDate) => {
    const currentDate = selectedDate || date;
    setDate(currentDate);
    setShow(false);
    var nesto = queryString.stringify(currentDate);
    console.log(nesto);
    axios
      .get(`http://cfc0f7aca710.ngrok.io/api/temperatures/filter`)
      .then((response) => {
        console.log(response.data);
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
});
