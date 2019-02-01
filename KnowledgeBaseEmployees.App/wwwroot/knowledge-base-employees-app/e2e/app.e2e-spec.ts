import { KnowledgeBaseEmployeesAppPage } from './app.po';

describe('knowledge-base-employees-app App', () => {
  let page: KnowledgeBaseEmployeesAppPage;

  beforeEach(() => {
    page = new KnowledgeBaseEmployeesAppPage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
