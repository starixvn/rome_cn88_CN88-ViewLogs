<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:xslLogFormat="urn:LogFormat">
  <xsl:import href="add_update_possess.xslt" />
  <xsl:output method="html" indent="no" />

  <xsl:template match="r">
    <xsl:call-template name="addOrUpdatePossess">
      <xsl:with-param name="actionId" select="'105000'" />
      <xsl:with-param name="isAgent" select="false()" />
    </xsl:call-template>
  </xsl:template>
</xsl:stylesheet>
