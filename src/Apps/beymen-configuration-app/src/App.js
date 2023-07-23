import * as React from "react";
import ConfigurationList from "./components/configuration-list-components";
import ConfigurationFilter from "./components/configuration-filter-components";
import ConfigurationModal from "./components/configuration-modal-components";
import { Add, GetAll, Update } from "./services/ConfigurationService";
import { useDispatch } from "react-redux";
import {
  setConfigurationData,
  setConfigurationDialog,
  setConfigurationMessage,
} from "./features/configurationSlice";
import SnackBarComponents from "./components/snackbar-components";

const App = () => {
  const dispatch = useDispatch();

  const getConfigurationList = React.useCallback(async () => {
    const response = await GetAll();
    if (response.status === 200) {
      dispatch(setConfigurationData(response.data));
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  React.useEffect(() => {
    getConfigurationList();
  }, [getConfigurationList]);

  const onSave = async (record) => {
    if (record.isUpdate) {
      onUpdate(record);
    } else {
      onAdd(record);
    }
  };

  const onAdd = (record) => {
    Add(record)
      .then((response) => {
        if (response.data) {
          getConfigurationList();
          dispatch(
            setConfigurationMessage({
              text: "The record has been successfully added.",
              severity: "success",
            })
          );
          dispatch(setConfigurationDialog(null));
        } else {
          dispatch(
            setConfigurationMessage({
              text: "The record could not be added.",
              severity: "error",
            })
          );
        }
      })
      .catch((error) => {
        dispatch(
          setConfigurationMessage({
            text: error.message,
            severity: "error",
          })
        );
      });
  };

  const onUpdate = (record) => {
    Update(record)
      .then((response) => {
        if (response.data) {
          getConfigurationList();
          dispatch(
            setConfigurationMessage({
              text: "The record has been successfully updated.",
              severity: "success",
            })
          );
          dispatch(setConfigurationDialog(null));
        } else {
          dispatch(
            setConfigurationMessage({
              text: "The record could not be updated.",
              severity: "error",
            })
          );
        }
      })
      .catch((error) => {
        dispatch(
          setConfigurationMessage({
            text: error.message,
            severity: "error",
          })
        );
      });
  };

  return (
    <div className="max-w-2xl mx-auto mt-12">
      <ConfigurationFilter />
      <ConfigurationList />
      <ConfigurationModal onSave={onSave} />
      <SnackBarComponents />
    </div>
  );
};
export default App;
