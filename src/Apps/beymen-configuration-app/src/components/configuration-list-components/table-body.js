import { TableBody, TableCell, TableRow } from "@mui/material";
import { useSelector } from "react-redux";
import CheckBoxOutlineBlankIcon from "@mui/icons-material/CheckBoxOutlineBlank";
import CheckBoxOutlinedIcon from "@mui/icons-material/CheckBoxOutlined";
import React from "react";
import EditButton from "./button-edit";
import DeleteButton from "./button-delete";

const TableBodyComponent = () => {
  const configurationData = useSelector(
    (state) => state.configuration.configurationData
  );

  return (
    <TableBody>
      {configurationData &&
        configurationData.map((item, index) => {
          return (
            <TableRow key={index}>
              <TableCell>{index + 1}</TableCell>
              <TableCell>{item.applicationName}</TableCell>
              <TableCell>{item.name}</TableCell>
              <TableCell>{item.type}</TableCell>
              <TableCell>{item.value}</TableCell>
              <TableCell>
                {item.isActive ? (
                  <CheckBoxOutlinedIcon />
                ) : (
                  <CheckBoxOutlineBlankIcon />
                )}
              </TableCell>
              <TableCell>
                <div className="flex">
                  <EditButton item={item} />
                  <DeleteButton item={item} />
                </div>
              </TableCell>
            </TableRow>
          );
        })}
    </TableBody>
  );
};

export default React.memo(TableBodyComponent);
