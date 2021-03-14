import React from "react";
import { View, Text } from "react-native";

const TemperatureData = (props) => {
  return (<View >
    <Text>Temperature: {props.temperature.temperatureCelsius}</Text>
  </View>)
};

export default TemperatureData;

