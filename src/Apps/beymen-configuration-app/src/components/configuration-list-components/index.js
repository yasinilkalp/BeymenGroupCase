import * as React from "react";
import Table from "@mui/material/Table";
import TableContainer from "@mui/material/TableContainer";
import Paper from "@mui/material/Paper";
import TableHeader from "./table-header";
import TableBody from "./table-body";

const ConfigurationList = () => {
  return (
    <TableContainer component={Paper}>
      <Table>
        <TableHeader />
        <TableBody />
      </Table>
    </TableContainer>
  );
};

export default ConfigurationList;
