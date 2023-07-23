import { createSlice } from "@reduxjs/toolkit";

const initialState = {
  configurationData: [],
  configurationSearch: {
    keyword: "",
    filterApplication: "all",
  },
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
    setConfigurationSearch: (state, action) => {
      state.configurationSearch = action.payload;
    },
  },
});

export const {
  setConfigurationData,
  setConfigurationDialog,
  setConfigurationDataRefresh,
  setConfigurationMessage,
  setConfigurationSearch,
} = configurationSlice.actions;

export default configurationSlice.reducer;
