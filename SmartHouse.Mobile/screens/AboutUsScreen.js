import React from "react";
import { View, Text, StyleSheet, FlatList } from "react-native";

const AboutUsScreen = () => {
  return (
    <View style={styles.container}>
      <Text style={{fontSize: 16, marginTop: 20}}>Smart house is an IoT project for final thesis.</Text>
      <Text style={{fontSize: 16 }}>It gives different features like:</Text>
      <FlatList
      style={{marginTop:15, fontSize:14}}
        data={[
          { key: "Temperature history" },
          { key: "Setting up temperature treshold" },
          { key: "Window opening automatization" },
          { key: "SMS emergency notifications" },
        ]}
        renderItem={({ item }) => <Text>{'\u2022'} {item.key}</Text>}
      />
    </View>
  );
};

export default AboutUsScreen;

const styles = StyleSheet.create({
  container: {
    display: "flex",
    alignItems: 'center',
    top: '40%',
  },
});
