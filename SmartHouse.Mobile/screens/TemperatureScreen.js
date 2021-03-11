import React, { useEffect, useState } from "react";
import { View, Text, StyleSheet } from "react-native";
import axios from "axios";
import Temperature from "../src/components/Temperature/Temperature";

const TemperatureScreen = () => {
  const [isLoading, setLoading] = useState(true);
  const [temperature, setTemperature] = useState(0.0);

  useEffect(() => {
    axios
      .get("http://cfc0f7aca710.ngrok.io/api/temperatures/current")
      .then((response) => {
        setLoading(false);
        setTemperature(response.data.temperatureCelsius);
      })
      .catch((error) => console.log(error));
  });

  return (
    <View style={styles.container}>
      <View style={styles.headerContainer}>
        {isLoading ? (
          <Text style={styles.header}>Fetching current temperature</Text>
        ) : (
          <Text style={styles.header}>
            Current temperature is: {temperature}°C
          </Text>
        )}
      </View>
      <Temperature />
    </View>
  );
};

export default TemperatureScreen;

const styles = StyleSheet.create({
  container: {
    flex: 1,
  },
  headerContainer: {
    alignItems: "center",
  },
  header: {
    fontSize: 20,
    marginTop: 20,
  },
});
