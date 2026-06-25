// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

import { ComponentType } from 'react';
import { Column } from 'primereact/column';
import { DataPage, MenuItem } from '@cratis/components/DataPage';
import { useDialog } from '@cratis/arc.react/dialogs';
import * as mdIcons from 'react-icons/md';
import { ObserveAll } from './Listing/ObserveAll';
import { HireEmployeeDialog } from './Hire/HireEmployeeDialog';

/**
 * Page that lists all employees and allows hiring new ones.
 * @returns The rendered page.
 */
export const Employees = () => {
    const [HireEmployeeDialogWrapper, showHireEmployeeDialog] = useDialog(
        HireEmployeeDialog as unknown as ComponentType<object>);

    return (
        <>
            <DataPage
                title="Employees"
                query={ObserveAll}
                dataKey="id"
                emptyMessage="No employees have been hired yet">
                <DataPage.MenuItems>
                    <MenuItem
                        label="Hire employee"
                        icon={mdIcons.MdPersonAdd}
                        command={() => { void showHireEmployeeDialog(); }}
                    />
                </DataPage.MenuItems>
                <DataPage.Columns>
                    <Column field="name" header="Name" />
                    <Column field="department" header="Department" />
                </DataPage.Columns>
            </DataPage>
            <HireEmployeeDialogWrapper />
        </>
    );
};
