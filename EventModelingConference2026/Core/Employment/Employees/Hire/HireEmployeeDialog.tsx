// Copyright (c) Cratis. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

import { HireEmployee } from './HireEmployee';
import { CommandDialog } from '@cratis/components/CommandDialog';
import { InputTextField } from '@cratis/components/CommandForm';

/**
 * Dialog for hiring a new employee.
 * @returns The rendered dialog.
 */
export const HireEmployeeDialog = () => {
    return (
        <CommandDialog
            command={HireEmployee}
            title="Hire employee"
            okLabel="Hire"
            cancelLabel="Cancel">
            <InputTextField<HireEmployee> value={command => command.name} title="Name" placeholder="Name" />
            <InputTextField<HireEmployee> value={command => command.department} title="Department" placeholder="Department" />
        </CommandDialog>
    );
};
