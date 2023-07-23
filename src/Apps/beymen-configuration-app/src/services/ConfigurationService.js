import axios from "axios";

var applicationNames = ["ServiceA", "ServiceB"];
export default applicationNames;

export function GetAll() {
  return axios.get(process.env.REACT_APP_API_URL + "/api/Configuration/GetAll");
}

export function Get(key) {
  return axios.get(
    process.env.REACT_APP_API_URL + `/api/Configuration/Get/${key}`
  );
}

export function Delete(key) {
  return axios.delete(
    process.env.REACT_APP_API_URL + `/api/Configuration/Delete/${key}`
  );
}

export function Add(model) {
  return axios.post(
    process.env.REACT_APP_API_URL + `/api/Configuration/Add`,
    model
  );
}

export function Update(model) {
  return axios.put(
    process.env.REACT_APP_API_URL + `/api/Configuration/Update`,
    model
  );
}
