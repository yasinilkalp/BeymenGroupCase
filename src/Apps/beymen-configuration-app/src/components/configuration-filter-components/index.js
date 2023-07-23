import {
  Button,
  FormControl,
  Grid,
  MenuItem,
  Select,
  TextField,
} from "@mui/material";
import AddIcon from "@mui/icons-material/Add";
import { useDispatch, useSelector } from "react-redux";
import {
  setConfigurationDataRefresh,
  setConfigurationDialog,
  setConfigurationSearch,
} from "../../features/configurationSlice";
import { applicationNames } from "../../services/ConfigurationService";

const ConfigurationFilter = () => {
  const dispatch = useDispatch();
  const configurationSearch = useSelector(
    (state) => state.configuration.configurationSearch
  );

  const onChange = (key, value) => {
    dispatch(setConfigurationSearch({ ...configurationSearch, [key]: value }));
    dispatch(setConfigurationDataRefresh(true));
  };

  return (
    <div className="flex justify-between mb-3">
      <div className="grow mr-4">
        <Grid container spacing={2}>
          <Grid item xs={4}>
            <FormControl fullWidth size="small" className="bg-white">
              <Select
                value={configurationSearch.filterApplication}
                size="small"
                onChange={(e) => {
                  console.log(e.target.value);
                  onChange("filterApplication", e.target.value);
                }}
              >
                <MenuItem key="all" value="all">
                  All Applications
                </MenuItem>
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
          <Grid item xs={8}>
            <TextField
              size="small"
              fullWidth
              value={configurationSearch && configurationSearch.keyword}
              onChange={(e) => onChange("keyword", e.target.value)}
              placeholder="Search"
              className="bg-white"
            />
          </Grid>
        </Grid>
      </div>
      <AddButton />
    </div>
  );
};

const AddButton = () => {
  var dispatch = useDispatch();

  const addClick = () => {
    dispatch(
      setConfigurationDialog({
        isUpdate: false,
      })
    );
  };

  return (
    <Button size="small" className="add-button" onClick={addClick}>
      <AddIcon className="w-2 mr-2" />
      Add Record
    </Button>
  );
};

export default ConfigurationFilter;
