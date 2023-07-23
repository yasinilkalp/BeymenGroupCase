import { combineReducers } from "redux";
import configurationSlice from "../../features/configurationSlice";

export const rootReducer = combineReducers({
  configuration: configurationSlice,
});
