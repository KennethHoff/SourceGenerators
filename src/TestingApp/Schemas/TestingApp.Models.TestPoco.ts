import { z } from 'zod';

export const TestingApp.Models.TestPocoSchema = z.object({
	Name: string,
});

export type TestingApp.Models.TestPocoSchemaType = z.infer<typeof TestingApp.Models.TestPocoSchema>;