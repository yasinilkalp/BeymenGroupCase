import { createSlice } from "@reduxjs/toolkit";

const initialState = {
  configurationData: [],
  configurationDialog: null,
  configurationMessage: null,
  configurationDataRefresh: false,
};
const configurationSlice = createSlice({
  name: "configuration",
  initialState,
  reducers: {
    setConfigurationData: (state, action) => {
      state.configurationData = action.payload;
    },
    setConfigurationDialog: (state, action) => {
      state.configurationDialog = action.payload;
    },
    setConfigurationDataRefresh: (state, action) => {
      state.configurationDataRefresh = action.payload;
    },
    setConfigurationMessage: (state, action) => {
      state.configurationMessage = action.payload;
    },
  },
});

export const {
  setConfigurationData,
  setConfigurationDialog,
  setConfigurationDataRefresh,
  setConfigurationMessage,
} = configurationSlice.actions;

export default configurationSlice.reducer;
