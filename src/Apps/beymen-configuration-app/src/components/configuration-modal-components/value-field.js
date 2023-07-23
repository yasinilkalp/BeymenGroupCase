import {
  Checkbox,
  FormControlLabel,
  FormGroup,
  TextField,
} from "@mui/material";
import { NumericFormat } from "react-number-format";

const ValueField = (props) => {
  const { values, onChange, onBlur, errors } = props;

  return (
    <>
      {values.type === "String" && (
        <StringValueField {...{ values, onChange, onBlur, errors }} />
      )}
      {values.type === "Boolean" && (
        <BooleanValueField {...{ values, onChange, onBlur, errors }} />
      )}
      {values.type === "Int" && (
        <IntValueField {...{ values, onChange, onBlur, errors }} />
      )}
      {values.type === "Decimal" && (
        <DecimalValueField {...{ values, onChange, onBlur, errors }} />
      )}
    </>
  );
};

const StringValueField = (props) => {
  const { values, onChange, onBlur, errors } = props;
  return (
    <TextField
      size="small"
      fullWidth
      label="Value"
      value={values.value}
      onChange={(e) => onChange("value", e.target.value)}
      onBlur={(e) => onBlur("value", e)}
      error={Boolean(errors.value)}
    />
  );
};

const DecimalValueField = (props) => {
  const { values, onChange, onBlur, errors } = props;
  return (
    <NumericFormat
      size="small"
      decimalScale={2}
      fixedDecimalScale
      fullWidth
      label="Value"
      value={values.value}
      onChange={(e) => onChange("value", e.target.value)}
      onBlur={(e) => onBlur("value", e)}
      error={Boolean(errors.value)}
      customInput={TextField}
    />
  );
};

const IntValueField = (props) => {
  const { values, onChange, onBlur, errors } = props;
  return (
    <NumericFormat
      size="small"
      fullWidth
      allowLeadingZeros
      decimalScale={0}
      fixedDecimalScale
      label="Value"
      value={values.value}
      onChange={(e) => onChange("value", e.target.value)}
      onBlur={(e) => onBlur("value", e)}
      error={Boolean(errors.value)}
      customInput={TextField}
    />
  );
};

const BooleanValueField = (props) => {
  const { values, onChange } = props;
  return (
    <FormGroup className="Checkbox">
      <FormControlLabel
        checked={values.value === "True"}
        onChange={(e) => {
          onChange("value", e.target.checked ? "True" : "False");
        }}
        control={<Checkbox />}
        label="Value"
      />
    </FormGroup>
  );
};

export default ValueField;
