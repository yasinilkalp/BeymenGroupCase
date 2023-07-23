import React from "react";
import { useSelector } from "react-redux";
import { useDispatch } from "react-redux";
import DialogComponent from "../dialog-component";
import { useEffect } from "react";
import { setConfigurationDialog } from "../../features/configurationSlice";
import {
  Checkbox,
  FormControl,
  FormControlLabel,
  FormGroup,
  Grid,
  InputLabel,
  MenuItem,
  Select,
  TextField,
} from "@mui/material";
import applicationNames from "../../services/ConfigurationService";
let initialValues = {
  name: "",
  type: "",
  value: "",
  applicationName: "",
  isUpdate: false,
};
const ConfigurationModal = (props) => {
  const { onSave } = props;
  const dispatch = useDispatch();
  const [values, setValues] = React.useState(initialValues);
  const [errors, setErrors] = React.useState({
    name: "",
    type: "",
    value: "",
    applicationName: "",
  });
  const configurationDialog = useSelector(
    (state) => state.configuration.configurationDialog
  );

  const errorMessages = {
    name: "Name is required",
    type: "Type is required",
    value: "Value is required",
    applicationName: "ApplicationName is required",
  };

  useEffect(() => {
    if (configurationDialog && configurationDialog.isUpdate) {
      setValues(configurationDialog);
    } else {
      setValues(initialValues);
    }
  }, [configurationDialog]);

  const onChange = (name, value) => {
    setValues((prevState) => ({
      ...prevState,
      [name]: value,
    }));

    setErrors((prevState) => ({
      ...prevState,
      [name]: "",
    }));
  };

  const onBlur = (name, e) => {
    let value = e && e.target ? e.target.value : values[name];

    if (!value || value === "") {
      setErrors((prevState) => ({
        ...prevState,
        [name]: errorMessages[name],
      }));
    } else {
      if (errors[name]) {
        setErrors((prevState) => ({
          ...prevState,
          [name]: "",
        }));
      }
    }
  };

  const validate = () => {
    let isError = false;
    const error = {
      name: "",
      type: "",
      value: "",
      applicationName: "",
    };
    const { name, type, value, applicationName } = values;

    if (!name || name === "") {
      isError = true;
      error.name = errorMessages.name;
    }
    if (!type || type === "") {
      isError = true;
      error.type = errorMessages.type;
    }
    if (!value || value === "") {
      isError = true;
      error.value = errorMessages.value;
    }
    if (!applicationName || applicationName === "") {
      isError = true;
      error.applicationName = errorMessages.applicationName;
    }

    setErrors(error);
    return isError;
  };

  const onSubmit = async (e) => {
    e.preventDefault();
    const err = validate();
    if (!err) {
      await onSave(values);
    }
  };

  return configurationDialog ? (
    <DialogComponent
      data={configurationDialog}
      maxWidth="xs"
      title={configurationDialog.isUpdate ? "Update Record" : "Add Record"}
      handleClose={() => dispatch(setConfigurationDialog(null))}
      handleSave={onSubmit}
    >
      <Grid container direction="column" spacing={2}>
        <Grid item>
          <FormControl fullWidth size="small">
            <InputLabel id="applicationName">ApplicationName</InputLabel>
            <Select
              id="applicationName"
              value={values.applicationName}
              size="small"
              label="ApplicationName"
              onBlur={(e) => onBlur("applicationName", e)}
              error={Boolean(errors.applicationName)}
              onChange={(e) => {
                onChange("applicationName", e.target.value);
              }}
            >
              {applicationNames.map((name) => {
                return (
                  <MenuItem key={name} value={name}>
                    {name}
                  </MenuItem>
                );
              })}
            </Select>
          </FormControl>
        </Grid>
        {values.applicationName && (
          <>
            <Grid item>
              <TextField
                size="small"
                fullWidth
                label="Name"
                value={values.name}
                onChange={(e) => onChange("name", e.target.value)}
                onBlur={(e) => onBlur("name", e)}
                error={Boolean(errors.name)}
              />
            </Grid>
            <Grid item>
              <TextField
                size="small"
                fullWidth
                label="Type"
                value={values.type}
                onChange={(e) => onChange("type", e.target.value)}
                onBlur={(e) => onBlur("type", e)}
                error={Boolean(errors.type)}
              />
            </Grid>
            <Grid item>
              <TextField
                size="small"
                fullWidth
                label="Value"
                value={values.value}
                onChange={(e) => onChange("value", e.target.value)}
                onBlur={(e) => onBlur("value", e)}
                error={Boolean(errors.value)}
              />
            </Grid>
            <Grid item>
              <FormGroup className="Checkbox">
                <FormControlLabel
                  checked={values.isActive}
                  onChange={(e) => {
                    onChange("isActive", e.target.checked);
                  }}
                  control={<Checkbox />}
                  label="Is Active"
                />
              </FormGroup>
            </Grid>
          </>
        )}
      </Grid>
    </DialogComponent>
  ) : (
    <></>
  );
};

export default ConfigurationModal;
